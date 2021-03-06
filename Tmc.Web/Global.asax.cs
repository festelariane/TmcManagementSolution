﻿using FluentValidation.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tmc.BLL.Impl.ScheduleJobs;
using Tmc.Core.Infrastructure;
using Tmc.Web.Framework.Common;
using Tmc.Web.Framework.FluentValidation;
using Tmc.Web.Framework.Models;
using Tmc.Web.Framework.Themes;

namespace Tmc.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            EngineContext.Initialize(false);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new TmcThemeableRazorViewEngine());
            DependencyResolver.SetResolver(new TmcDependencyResolver());

            ModelBinders.Binders.Add(typeof(BaseModel), new TmcModelBinder());
            var utcDateTimeModel = new UTCDateTimeModelBinder();
            ModelBinders.Binders.Add(typeof(DateTime), utcDateTimeModel);
            ModelBinders.Binders.Add(typeof(DateTime?), utcDateTimeModel);

            var validationProviderFactory = new TmcValidatorFactory();
            var validationProvider = new FluentValidationModelValidatorProvider(validationProviderFactory);
            validationProvider.AddImplicitRequiredValidator = false;
            ModelValidatorProviders.Providers.Add(validationProvider);

            var jobs = EngineContext.Current.ContainerManager.Resolve<TestAllJobs>();
            jobs.Start();
        }
    }
}
