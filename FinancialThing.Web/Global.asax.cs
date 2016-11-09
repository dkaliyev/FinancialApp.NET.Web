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
using FinancialThing.Filters;
using FinancialThing.Utilities;
using FinancialThing.Web.DataAccess;
using NHibernate;
using NHibernate.Cfg;
using FinancialThing.Models;

namespace FinancialThing
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.Register(x => new AsyncHttpClient()).As<IDataGrabber>().SingleInstance();
            builder.Register(x => new CompanyRepository(x.Resolve<IDataGrabber>())).As<ICompanyRepository>().SingleInstance(); 
            builder.Register(x => new DataRepository(x.Resolve<IDataGrabber>())).As<IRepository<Company, Guid>>().SingleInstance();
            builder.Register(x => new SectorRepository(x.Resolve<IDataGrabber>())).As<IRepository<Sector, Guid>>().SingleInstance();
            builder.Register(x => new IndustryRepository(x.Resolve<IDataGrabber>())).As<IRepository<Industry, Guid>>().SingleInstance(); 
            builder.Register(x => new RatioRepository(x.Resolve<IDataGrabber>())).As<IRepository<Ratio, Guid>>().SingleInstance(); 
            builder.Register(x => new RatioValueRepository(x.Resolve<IDataGrabber>())).As<IRepository<RatioValue, Guid>>().SingleInstance(); 
            builder.Register(x => new DictionaryRepository(x.Resolve<IDataGrabber>())).As<IRepository<Dictionary, Guid>>().SingleInstance(); 
            builder.Register(x => new StockExchangeRepository(x.Resolve<IDataGrabber>())).As<IRepository<StockExchange, Guid>>().SingleInstance(); 
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
