using Oscallo.Castle.AddonedKernel.Injectors;

namespace DataServer.Core.Demo.DI.IntegrableInterfaces
{
    internal interface IHaveDependencies
    {
        public static abstract void ReginsterDependencies(IRegistrar registrar);
    }
}
