using System;
using System.Configuration;
using CrossCutting.Core.IOC;
using CrossCutting.Core.Logging;
using CrossCutting.MainModule.Logging;
using Microsoft.Practices.Unity;
using Undead.Music.Data;
using Undead.Music.Application;

namespace CrossCutting.MainModule.IOC
{
    public class IocUnityContainer : IContainer
    {
        private static UnityContainer _unityContainer;

        public IocUnityContainer() : this(new UnityContainer())
        {}

        public IocUnityContainer(UnityContainer container)
        {
            _unityContainer = container;
            RegisterTypes();
        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _unityContainer.Resolve(type);
        }

        public static void RegisterTypes()
        {
            bool realContainer = true;
            if (ConfigurationManager.AppSettings["IocRealContainer"] != null)
            {
                if (bool.TryParse(ConfigurationManager.AppSettings["IocRealContainer"], out realContainer) == false)
                {
                    realContainer = true;
                }
            }

            if (realContainer)
            {
                RegisterRealTypes();
            }
        }

        private static void RegisterRealTypes()
        {
            _unityContainer.RegisterType<ILogService, FileLogService>();
            _unityContainer.RegisterType<IUnitOfWork, UnitOfWork>();
            _unityContainer.RegisterType<ISongService, SongService>();
            _unityContainer.RegisterType<IHostService, HostService>();
        }
    }
}