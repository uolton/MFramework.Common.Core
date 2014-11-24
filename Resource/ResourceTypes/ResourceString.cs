using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Resource.ResourceTypes
{
    public   class ResourceString:ResourceBase<String>
    {
        private string _name;
        public ResourceString(string name)
        {
            _name = name;

        }

        public override String Value
        {
            get { return _rc.GetString(_name); }
            
        }
    }
}
