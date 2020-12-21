using Microsoft.Extensions.DependencyInjection;

namespace VH.Core.Mapping
{
    public static class ConfigureServiceExtensions
    {
        public static IServiceCollection AddMapperRegistrations(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMapperFactory, MapperFactory>();

            serviceCollection.Scan(scan => scan.FromAssembliesOf(typeof(IMapTo<,>))
                .AddClasses(classes => classes.AssignableTo(typeof(IMapTo<,>))
                    .Where(_ => !_.IsGenericType))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            serviceCollection.TryDecorate(typeof(IMapTo<,>), typeof(MapperLoggingDecorator<,>));

            serviceCollection.Scan(scan => scan.FromAssembliesOf(typeof(IMapTo<,,>))
                .AddClasses(classes => classes.AssignableTo(typeof(IMapTo<,,>))
                    .Where(_ => !_.IsGenericType))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            serviceCollection.TryDecorate(typeof(IMapTo<,,>), typeof(MapperLoggingDecorator<,,>));

            serviceCollection.Scan(scan => scan.FromAssembliesOf(typeof(IMapTo<,,,>))
                .AddClasses(classes => classes.AssignableTo(typeof(IMapTo<,,,>))
                    .Where(_ => !_.IsGenericType))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            serviceCollection.TryDecorate(typeof(IMapTo<,,,>), typeof(MapperLoggingDecorator<,,,>));

            serviceCollection.Scan(scan => scan.FromAssembliesOf(typeof(IMapTo<,,,,>))
                .AddClasses(classes => classes.AssignableTo(typeof(IMapTo<,,,,>))
                    .Where(_ => !_.IsGenericType))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            serviceCollection.TryDecorate(typeof(IMapTo<,,,,>), typeof(MapperLoggingDecorator<,,,,>));

            return serviceCollection;
        }
    }
}
