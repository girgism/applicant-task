using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Applicants.Infrastructure.DbConfiguration;
public static class DatabaseServiceConfiguration
{
    public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicantsContext>(options =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseSqlServer(configuration.GetConnectionString("DBConnection"), x =>
            {

            })
            .EnableSensitiveDataLogging();
        });

        return services;
    }
}
