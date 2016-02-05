using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using FinancialThing.DataAccess;
using FinancialThing.DataAccess.nHibernate;
using FinancialThing.Filters;
using FinancialThing.Utilities;
using FinancialThing.Web.DataAccess;
using NHibernate;
using NHibernate.Cfg;

namespace FinancialThing
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.Register(x => new FTDataGrabber()).As<IDataGrabber>().SingleInstance();
            builder.Register(x => new CompanyRepository(x.Resolve<IDataGrabber>())).As<ICompanyServiceRepository>().SingleInstance(); 
            builder.Register(x => new DataRepository(x.Resolve<IDataGrabber>())).As<IDataServiceRepository>().SingleInstance();
            builder.Register(x => new SectorRepository(x.Resolve<IDataGrabber>())).As<ISectorServiceRepository>().SingleInstance();
            builder.Register(x => new IndustryRepository(x.Resolve<IDataGrabber>())).As<IIndustryServiceRepository>().SingleInstance(); 
            builder.Register(x => new RatioRepository(x.Resolve<IDataGrabber>())).As<IRatioServiceRepository>().SingleInstance(); 
            builder.Register(x => new RatioValueRepository(x.Resolve<IDataGrabber>())).As<IRatioValueServiceRepository>().SingleInstance(); 
            builder.Register(x => new DictionaryRepository(x.Resolve<IDataGrabber>())).As<IDictionaryServiceRepository>().SingleInstance(); 
            builder.Register(x => new StockExchangeRepository(x.Resolve<IDataGrabber>())).As<IStockExchangeServiceRepository>().SingleInstance(); 
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
