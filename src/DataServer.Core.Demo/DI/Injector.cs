using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Oscallo.Castle.AddonedKernel.Injectors;
using System;

namespace DataServer.Core.Demo.DI
{
    public class Injector : IInjector
    {
		private readonly IWindsorContainer _WindsorContainer;

		public Injector(IWindsorContainer windsorContainer)
		{
			this._WindsorContainer = windsorContainer;
		}

		public static void Register(IWindsorContainer windsorContainer, params IRegistration[] registrations)
		{
			windsorContainer.Register(registrations);
		}

		public static T Resolve<T>(IWindsorContainer windsorContainer)
		{
			return windsorContainer.Resolve<T>();
		}

		void IRegistrar.AddFacility<TFacility>()
		{
			this._WindsorContainer.AddFacility<TFacility>();
		}

		void IRegistrar.AddFacilityIfAbsent<TFacility>()
		{
			IRegistrar dIRegistrar = this;
			if (((IRegistrar)(this)).HasFacility<TFacility>() == false)
			{
				dIRegistrar.AddFacility<TFacility>();
			}
		}

		void IDisposable.Dispose() => this._WindsorContainer?.Dispose();

		public bool HasComponent<T>()
		{
			return this._WindsorContainer.Kernel.HasComponent(typeof(T));
		}

		public bool HasFacility<TFacility>() where TFacility : IFacility, new()
		{
			foreach (IFacility facility in this._WindsorContainer.Kernel.GetFacilities())
			{
				if (facility.GetType() == typeof(TFacility))
				{
					return true;
				}
			}
			return false;
		}

		void IRegistrar.Register(params IRegistration[] registrations)
		{
			this._WindsorContainer.Register(registrations);
		}

		void IRegistrar.RegisterIfAbsent<T>(params IRegistration[] registrations)
		{
			IRegistrar dIRegistrar = this;
			if (HasComponent<T>() == false)
			{
				dIRegistrar.Register(registrations);
			}
		}

		void IContainerRegistrar.AddChildContainer(IWindsorContainer windsorContainer) => this._WindsorContainer.AddChildContainer(windsorContainer);

		void IContainerRegistrar.RemoveChildContainer(IWindsorContainer windsorContainer) => this._WindsorContainer.RemoveChildContainer(windsorContainer);

		T IResolver.Resolve<T>() => this._WindsorContainer.Resolve<T>();

		T IResolver.Resolve<T>(string key) => this._WindsorContainer.Resolve<T>(key);

		T IResolver.Resolve<T>(Arguments args) => this._WindsorContainer.Resolve<T>(args);

		public virtual void Dispose()
		{
			((IDisposable)this).Dispose();
		}
	}
}
