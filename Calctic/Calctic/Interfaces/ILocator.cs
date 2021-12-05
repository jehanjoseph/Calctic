using System;

namespace Calctic.Interfaces
{
    public interface ILocator
    {
        T GetInstance<T>()
            where T : class;

        void Register<T>()
            where T : class;

        void Register<T>(T instance)
            where T : class;

        void Register<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface;

        void RegisterAsSingleton<T>()
            where T : class;
    }
}
