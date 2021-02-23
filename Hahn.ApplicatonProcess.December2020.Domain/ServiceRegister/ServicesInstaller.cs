using Hahn.ApplicatonProcess.December2020.Domain.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hahn.ApplicatonProcess.December2020.Domain.ServiceRegister
{
    public static class ServicesInstaller
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.TryAddTransient<IApplicantService, ApplicantService>();
            return services;
        }
    }
}
