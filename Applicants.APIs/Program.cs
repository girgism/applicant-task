using Applicants.APIs.Middlewares;
using Applicants.APIs.ServicesDependencyInjection;
using Applicants.Application.ServicesDependencyInjection;
using Applicants.Domain.Entities;
using Applicants.Infrastructure.DbConfiguration;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddDataBase(builder.Configuration);
builder.Services.AddServicesForAPIsLayer(builder.Configuration);
builder.Services.AddServicesForApplicationLayer();
// Add Data Protection before Identity
builder.Services.AddDataProtection();

// Identity Configuration
builder.Services.AddIdentityApiEndpoints<User>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<ApplicantsContext>()
                .AddSignInManager()
                .AddApiEndpoints();


builder.Services.Configure<BearerTokenOptions>(IdentityConstants.BearerScheme, opt =>
{
    opt.BearerTokenExpiration = TimeSpan.FromHours(1);
    opt.RefreshTokenExpiration = TimeSpan.FromMinutes(55500);
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.SignIn.RequireConfirmedEmail = false;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Applicants API", Version = "v1" });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
           Array.Empty<string>()
        }
    });
});


var app = builder.Build();


try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicantsContext>();
        dbContext.Database.Migrate();
        await SeedDefaultUser(scope);
    }
    async Task SeedDefaultUser(IServiceScope scope)
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

        // Create default user if not exists
        var defaultUser = await userManager.FindByNameAsync("admin");
        if (defaultUser == null)
        {
            var user = new User
            {
                UserName = "girgis@local.com",
                Email = "girgis@local.com",
                IsActive = true
            };
            var result = await userManager.CreateAsync(user, "password!11");
        }
    }
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.MapControllers();
    app.MapIdentityApi<User>();
    app.MapPost("/logout", async (SignInManager<User> signInManager) =>
    {
        await signInManager.SignOutAsync();
    }).RequireAuthorization();

    app.Run();
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred starting the application.");
    Environment.Exit(1);
}

