using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DataServer.Core.Demo.DI;
using Oscallo.Castle.AddonedKernel.Injectors;
using System.Windows;

namespace DataServer.Core.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() 
        {
            WindsorContainer container = new();

            Injector.Register(container, Component.For<IWindsorContainer>().Instance(container).LifeStyle.Singleton);
            Injector.Register(container, Component.For<IInjector>().ImplementedBy<Injector>().LifeStyle.Singleton);
        }
    }
}
