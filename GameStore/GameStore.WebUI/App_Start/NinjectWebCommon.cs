using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using GameStore.BLL.Filtering;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Logging;
using GameStore.BLL.Services;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Synchronizing;
using GameStore.DAL.UnitsOfWork;
using GameStore.WebUI;
using GameStore.WebUI.Filters;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Mvc.FilterBindingSyntax;
using WebActivatorEx;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof (NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof (NinjectWebCommon), "Stop")]

namespace GameStore.WebUI
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
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

                // register all your dependencies on the kernel container
                RegisterServices(kernel);

                // register the dependency resolver passing the kernel container
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

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
            kernel.Bind<IGameService>().To<GameService>();
            kernel.Bind<IGenreService>().To<GenreService>();
            kernel.Bind<ICommentService>().To<CommentService>();
            kernel.Bind<IPlatformTypeService>().To<PlatformTypeService>();
            kernel.Bind<IBasketService>().To<BasketService>();
            kernel.Bind<IPublisherService>().To<PublisherService>();
            kernel.Bind<IOrderService>().To<OrderService>();
            kernel.Bind<IPaymentService>().To<PaymentService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<ILanguageService>().To<LanguageService>();

            kernel.Bind<IPipeline<GameFilterContainer>>().To<Pipeline<GameFilterContainer>>();
            kernel.Bind<IFilter<GameFilterContainer>>().To<BaseFilter<GameFilterContainer>>();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();

            kernel.Bind<IDbSynchronizer>().To<DbSynchronizer>();

            kernel.Bind<ILogger>().To<GameStoreLogger>();

            kernel.BindFilter<PerformanceLoggingFilter>(FilterScope.Global, 0);
            kernel.BindFilter<ExceptionLoggingFilter>(FilterScope.Global, 0);
        }
    }
}