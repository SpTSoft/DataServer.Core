using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DataServer.Core.Demo.DI;
using DataServer.Core.Net;
using Oscallo.Castle.AddonedKernel.Injectors;
using System.Net;
using System.Windows;

namespace DataServer.Core.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		private readonly IInjector _Injector;

        public App() 
        {
            WindsorContainer container = new();

            Injector.Register(container, Component.For<IWindsorContainer>().Instance(container).LifeStyle.Singleton);
            Injector.Register(container, Component.For<IInjector>().ImplementedBy<Injector>().LifeStyle.Singleton);

			this._Injector = Injector.Resolve<IInjector>(container);

			this._Injector.RegisterIfAbsent<IGatewayListener>(Component.For<IGatewayListener>().ImplementedBy<GatewayListener>().LifeStyle.Singleton);

			Arguments args = new()
			{
				{ "iPAddress", IPAddress.Parse("127.0.0.1") }, {"Port", NetHelper.GetAvailablePort() }
			};

			IGatewayListener gatewayListener = this._Injector.Resolve<IGatewayListener>(args);

			gatewayListener.Run();
		}
    }
}
