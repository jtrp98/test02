[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(FingerprintPayment.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(FingerprintPayment.App_Start.NinjectWebCommon), "Stop")]

namespace FingerprintPayment.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using SchoolBright.Business.Classes;
    using SchoolBright.Business.Interfaces;
    using SchoolBright.DataAccess;
    using SchoolBright.DataAccess.Interfaces;
    using System;
    using System.Web;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IMappingService>().To<MappingService>().InRequestScope();
            kernel.Bind<ICommonService>().To<CommonService>().InRequestScope();
            kernel.Bind<ILoginService>().To<LoginService>().InRequestScope();
            kernel.Bind<IPlanService>().To<PlanService>().InRequestScope();
            kernel.Bind<ITimeTableSettingService>().To<TimeTableSettingService>().InRequestScope();
            kernel.Bind<IGradeService>().To<GradeService>().InRequestScope();
            kernel.Bind<IGraduationService>().To<GraduationService>().InRequestScope();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope().WithConstructorArgument("sEntities", (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["sEntities"] != null) ? HttpContext.Current.Session["sEntities"].ToString() : string.Empty );

            // Set static properties For Call the business class methods from WebMethod (Ajax or PageMethod)
            Helper.ServiceHelper.LoginService = kernel.Get<ILoginService>();
            Helper.ServiceHelper.PlanService = kernel.Get<IPlanService>();
            Helper.ServiceHelper.CommonService = kernel.Get<ICommonService>();
            Helper.ServiceHelper.GraduationService = kernel.Get<IGraduationService>();
        }        
    }
}
