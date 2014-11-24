using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Types
{
 
    
    public class Reference<T>:IHasValue<T>
    {
        private Func<T> _getter;
        private Action<T> _setter;
        public Reference(Func<T> getter, Action<T> setter)
        {
            _getter = getter;
            _setter = setter;
        }

        public T Value
        {
            get { return _getter(); }
            set { _setter(value); }
        }

        
    }
}
