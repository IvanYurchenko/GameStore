using System;
using System.Web;
using System.Web.Mvc;
using GameStore.BLL.Filtering;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Logging;
using GameStore.BLL.Services;
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
            kernel.Bind<IGameService>().To<GameService>();
            kernel.Bind<IGenreService>().To<GenreService>();
            kernel.Bind<ICommentService>().To<CommentService>();
            kernel.Bind<IPlatformTypeService>().To<PlatformTypeService>();
            kernel.Bind<IBasketService>().To<BasketService>();
            kernel.Bind<IPublisherService>().To<PublisherService>();
            kernel.Bind<IOrderService>().To<OrderService>();

            kernel.Bind<IPipeline<GameFilterContainer>>().To<Pipeline<GameFilterContainer>>();
            kernel.Bind<IFilter<GameFilterContainer>>().To<BaseFilter<GameFilterContainer>>();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            kernel.Bind<ILogger>().To<GameStoreLogger>();

            kernel.BindFilter<PerformanceLoggingFilter>(FilterScope.Global, 0);
            kernel.BindFilter<ExceptionLoggingFilter>(FilterScope.Global, 0);
        }
    }
}