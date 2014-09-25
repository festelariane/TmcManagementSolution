using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.DependencyManagement;
using Tmc.Core.Infrastructure;
using Autofac;
using Tmc.Core.Data;
using Tmc.Data;
using Autofac.Integration.Mvc;
using Tmc.BLL.Impl.Customers;
using Tmc.BLL.Contract.Customers;
using Tmc.BLL.Contract.Cards;
using Tmc.BLL.Impl.Cards;
using Tmc.Web.Framework.Routes;
using Tmc.Core.Common;
using Tmc.Data.DatabaseContext;
using Tmc.BLL.Impl.Transactions;
using Tmc.BLL.Contract.Transactions;
using Tmc.BLL.Impl.Security;
using Tmc.BLL.Contract.Security;
using Tmc.BLL.Contract.Authentication;
using Tmc.BLL.Impl.Authentication;
using System.Web;
using Tmc.BLL.Impl.ImportExport;
using Tmc.BLL.Contract.ImportExport;
using Quartz.Spi;
using Tmc.Web.Framework.Scheduler;
using Quartz.Impl;
using Quartz;
using System.Reflection;
using Tmc.BLL.Impl.ScheduleJobs;

namespace Tmc.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.Register(c => new HttpContextWrapper(HttpContext.Current) as HttpContextBase)
                .As<HttpContextBase>()
                .InstancePerRequest();

            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerRequest();

            var dataSettingsManager = new DataSettingsManager();

            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerRequest();
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            var dataProviderSettings = dataSettingsManager.LoadSettings();
            builder.Register(c => dataSettingsManager.LoadSettings()).As<DataSettings>();
            builder.Register(x => new EfDataProviderManager(x.Resolve<DataSettings>())).As<BaseDataProviderManager>().InstancePerDependency();
            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();

            builder.RegisterType<CustomerBiz>().As<ICustomerBiz>().InstancePerRequest();
            builder.RegisterType<CardTypeBiz>().As<ICardTypeBiz>().InstancePerRequest();
            builder.RegisterType<TransactionBiz>().As<ITransactionBiz>().InstancePerRequest();
            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerRequest();
            builder.RegisterType<CustomerRegistrationService>().As<ICustomerRegistrationService>().InstancePerRequest();
            builder.RegisterType<FormAuthenticationBiz>().As<IAuthenticationBiz>().InstancePerRequest();
            builder.RegisterType<ExportService>().As<IExportService>().InstancePerRequest();
            builder.RegisterType<RouteRegistrar>().As<IRouteRegistrar>().SingleInstance();

            var efDataProviderManager = new EfDataProviderManager(dataSettingsManager.LoadSettings());
            var dataProvider = efDataProviderManager.LoadDataProvider();
            dataProvider.InitConnectionFactory();

            builder.Register<IDbContext>(c => new TmcObjectContext(dataProviderSettings.DataConnectionString)).InstancePerRequest();


            builder.Register(c => new StdSchedulerFactory().GetScheduler())
               .As<IScheduler>()
               .InstancePerDependency(); // #1
            builder.Register(c => new AutofacJobFactory(EngineContext.Current.ContainerManager.Container))
                   .As<IJobFactory>();          // #2
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(p => typeof(IJob).IsAssignableFrom(p))
                   .PropertiesAutowired();      // #3
            builder.RegisterType(typeof(TestAllJobs)).As<TestAllJobs>();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
