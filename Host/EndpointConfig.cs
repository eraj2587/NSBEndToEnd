
using System.Data.Common;
using System.Data.Entity;
using System.Diagnostics;
using log4net.Config;
using Ninject;
using NServiceBus.Persistence.NHibernate;
using Storage.Infrastructure;
using Storage.Interface;
using Storage.Persistence;

namespace Host
{
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint, INeedInitialization, AsA_Server, IWantToRunBeforeConfigurationIsFinalized
    {
        IKernel CreateKernel()
        {
            var kernal = new StandardKernel();

            kernal.Bind<NServiceBus.Logging.ILog>().ToMethod(context => NServiceBus.Logging.LogManager.GetLogger(context.Request.Target.Member.DeclaringType));

            return kernal;
        }

        public void Customize(BusConfiguration configuration)
        {
            XmlConfigurator.Configure();
            configuration.UseContainer<NinjectBuilder>(k => k.ExistingKernel(CreateKernel()));
            configuration.RegisterComponents(x => x.ConfigureComponent<IRepository>(
                builder =>
                {
                    return new Repository(() =>
                    {
                        var nh = builder.Build<NHibernateStorageContext>();
                        var e = new OrderContext((DbConnection)nh.Connection);
                        return e;
                    });
                },
                DependencyLifecycle.InstancePerUnitOfWork));
            configuration.RegisterComponents(components => components.ConfigureComponent<UnitOfWork>(DependencyLifecycle.InstancePerUnitOfWork));


            configuration.UsePersistence<NHibernatePersistence>();
            configuration.UseTransport<MsmqTransport>();
            configuration.UseDataBus<FileShareDataBus>().BasePath(Constants.NServiceBus_DataBusBasePath);
            configuration.RijndaelEncryptionService();
            configuration.Transactions();
            configuration.EnableOutbox();
            configuration.Conventions().OrderConventions();
            //  
        }


        public void Run(Configure config)
        {
              SetupDevelopmentDatabase();
        }

        [Conditional("DEBUG")]
        void SetupDevelopmentDatabase()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<OrderContext>());
            using (var context = new OrderContext("NServiceBus/Persistence"))
                context.Database.Initialize(true);
        }


    }
}
