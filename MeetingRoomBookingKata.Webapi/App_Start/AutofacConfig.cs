using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using MeetingRoomBookingKata.Domain.Persistence;
using MeetingRoomBookingKata.Domain.Services;

namespace MeetingRoomBookingKata.WebApi
{
    /// <summary>
    /// Represent Autofac configuration.
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// Configured instance of <see cref="IContainer"/>
        /// <remarks><see cref="AutofacConfig.Configure"/> must be called before trying to get Container instance.</remarks>
        /// </summary>
        public static IContainer Container;

        /// <summary>
        /// Initializes and configures instance of <see cref="IContainer"/>.
        /// </summary>
        /// <param name="configuration"></param>
        public static void Configure(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            RegisterDependencies(builder);

            Container = builder.Build();

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }

        private static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<RoomProvider>().As<IRoomProvider>().InstancePerRequest();
            builder.RegisterType<UserProvider>().As<IUserProvider>().InstancePerRequest();
            builder.RegisterType<ReservationService>().As<IReservationService>().InstancePerRequest();

            builder.RegisterInstance(new MeetingRoomBookingKata.Persistence.InMemory.ReservationRepository()).As<IReservationRepository>();
        }
    }
}
