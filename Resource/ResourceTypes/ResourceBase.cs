using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Resource.ResourceTypes
{
    public abstract class ResourceBase<T>:IResource<T>
    {
        protected IResourceContainer _rc;
        public abstract T Value { get; }
        
        public IResource<T> Register(IResourceContainer c)
        {
            _rc = c;
            return this;
        }
    }
}
