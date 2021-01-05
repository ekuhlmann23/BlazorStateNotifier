using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStateNotifier
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBlazorStateNotifier(this IServiceCollection services, Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var stateNotifiers = assembly
                    .GetTypes()
                    .Where(p => p.IsAssignableTo(typeof(IStateNotifier))
                        && !p.IsAbstract && !p.IsInterface);

                foreach (var stateNotifier in stateNotifiers)
                {
                    services.AddScoped(stateNotifier);
                }
            }

            return services;
        }
    }
}
