using CrossCutting.MainModule.IOC;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Unity.Mvc5;

namespace Undead.Music.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            var unityContainer = new IocUnityContainer(container);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}