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

namespace Tmc.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
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


            builder.RegisterType<RouteRegistrar>().As<IRouteRegistrar>().SingleInstance();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
