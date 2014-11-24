using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Resource
{
    public static class ResourceManager<TResourceContainer>
        where TResourceContainer:IResourceContainer,new()
    {
        private static IResourceContainer _container;

        public static IResource<T> Register<T>(IResource<T> resource)
        {
            return resource.Register(_container);
        }
        static ResourceManager()
        {
            _container = new TResourceContainer();
        }
    }
}
