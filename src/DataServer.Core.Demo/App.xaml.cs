using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DataServer.Core.Demo.DI;
using DataServer.Core.Demo.MainMVVM;
using DataServer.Core.Net;
using DataServer.Core.Net.Args;
using DataServer.Core.Net.Settings;
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

			this._Injector.RegisterIfAbsent<IGatewayListenerSettings>(Component.For<IGatewayListenerSettings>().ImplementedBy<GatewayListenerSettings>().LifeStyle.Singleton.
				DependsOn(
					Dependency.OnValue("IPAddress", IPAddress.Parse("127.0.0.1")),
					Dependency.OnValue("Port", NetHelper.GetAvailablePort())
				));

			this._Injector.RegisterIfAbsent<IGatewayListenerArgsFactory>(Component.For<IGatewayListenerArgsFactory>().ImplementedBy<GatewayListenerArgsFactory>().LifeStyle.Singleton);
			this._Injector.RegisterIfAbsent<IGatewayListener>(Component.For<IGatewayListener>().ImplementedBy<GatewayListener>().LifeStyle.Singleton);

			this._Injector.RegisterIfAbsent<IMainViewModel>(Component.For<IMainViewModel>().ImplementedBy<MainViewModel>().LifeStyle.Singleton);
			this._Injector.RegisterIfAbsent<IMainWindow>(Component.For<IMainWindow>().ImplementedBy<MainWindow>().LifeStyle.Singleton);

			IMainViewModel mainViewModel = this._Injector.Resolve<IMainViewModel>();

			IGatewayListener gatewayListener = this._Injector.Resolve<IGatewayListener>();

			IMainWindow mainWindow = this._Injector.Resolve<IMainWindow>();

			mainWindow.Show();

			gatewayListener.Run();
		}
    }
}
