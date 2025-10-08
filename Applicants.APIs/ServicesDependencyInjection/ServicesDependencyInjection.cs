using Applicants.Domain.Interfaces;
using Applicants.Infrastructure.DbConfiguration;

namespace Applicants.APIs.ServicesDependencyInjection;

public static class ServicesDependencyInjection
{

    public static IServiceCollection AddServicesForAPIsLayer(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<ApplicantsContext>();
        services.AddScoped<IApplicantsContext>(option => option.GetService<ApplicantsContext>());

        var corsSettings = configuration.GetSection("Cors");
        var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
        var allowedHeaders = corsSettings.GetSection("AllowedHeaders").Get<string[]>() ?? ["Content-Type", "Authorization"];
        var allowedMethods = corsSettings.GetSection("AllowedMethods").Get<string[]>() ?? ["GET", "POST", "PUT", "DELETE", "OPTIONS"];

        services.AddCors(options =>
        {
            options.AddPolicy("ConfigurationPolicy", builder =>
            {
                builder.WithOrigins(allowedOrigins)
                       .WithHeaders(allowedHeaders)
                       .WithMethods(allowedMethods)
                       .AllowCredentials();
            });
        });

        return services;
    }
}
