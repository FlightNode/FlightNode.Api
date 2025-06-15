using FlightNode.Common.Utility;
using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Services.Models.Survey;
using Ganss.XSS;
using Microsoft.Practices.Unity;
using Owin;
using System.Linq;
using System.Reflection;

namespace FlightNode.DataCollection.Services.Providers
{
    public static class ApiStartup
    {
        public static IAppBuilder Configure(IAppBuilder app)
        {
            // No action to take. Leaving this stub as a reminder that we *could*
            // customize API startup for DataCollection, if needed.

            return app;
        }


        public static IUnityContainer ConfigureDependencyInjection(IUnityContainer container)
        {
            container = RegisterAllTypesIn(container, Assembly.GetExecutingAssembly());

            container.RegisterType<ISanitizer, Sanitizer>();

            return container;
        }

        private static IUnityContainer RegisterAllTypesIn(IUnityContainer container, Assembly repoAssembly)
        {
            var typesToRegister = AllClasses.FromAssemblies(repoAssembly)
                // Skipp registering ISurvey implementations
                .Where(t => !typeof(ISurvey).IsAssignableFrom(t) &&
                            !typeof(ISurveyModel).IsAssignableFrom(t))
                .ToList();

            container.RegisterTypes(typesToRegister,
                WithMappings.FromAllInterfacesInSameAssembly,
                WithName.Default,
                WithLifetime.PerResolve);

            return container;
        }

    }
}
