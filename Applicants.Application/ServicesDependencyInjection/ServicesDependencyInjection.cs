using Applicants.Application.Behaviors;
using Applicants.Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Applicants.Application.ServicesDependencyInjection;


public static class ServicesDependencyInjection
{
    public static IServiceCollection AddServicesForApplicationLayer(this IServiceCollection services)
    {

        var currentAssembly = typeof(ServicesDependencyInjection).Assembly;

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(currentAssembly);
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(currentAssembly);
        return services;
    }
}
