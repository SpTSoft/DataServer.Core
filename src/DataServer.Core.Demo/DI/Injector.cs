using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Oscallo.Castle.AddonedKernel.Injectors;
using System;

namespace DataServer.Core.Demo.DI
{
    public class Injector : IInjector
    {
        public static void Register(IWindsorContainer windsorContainer, params IRegistration[] registrations)
        {
            windsorContainer.Register(registrations);
        }

        public static T Resolve<T>(IWindsorContainer windsorContainer) 
        {
            return windsorContainer.Resolve<T>();
        }

        public void AddChildContainer(IWindsorContainer windsorContainer) => throw new NotImplementedException();

        public void AddFacility<TFacility>() where TFacility : IFacility, new() => throw new NotImplementedException();

        public void AddFacilityIfAbsent<TFacility>() where TFacility : IFacility, new() => throw new NotImplementedException();

        public void Dispose() => throw new NotImplementedException();

        public bool HasComponent<T>() => throw new NotImplementedException();

        public bool HasFacility<TFacility>() where TFacility : IFacility, new() => throw new NotImplementedException();

        public void Register(params IRegistration[] registrations) => throw new NotImplementedException();

        public void RegisterIfAbsent<T>(params IRegistration[] registrations) => throw new NotImplementedException();

        public void RemoveChildContainer(IWindsorContainer windsorContainer) => throw new NotImplementedException();

        public T Resolve<T>() => throw new NotImplementedException();
    }
}
