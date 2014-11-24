using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFramework.Common.Core.Extensions;
using MFramework.Common.Core.Types.Extensions;
namespace MFramework.Common.Core.ConditionalValues
{
    public class SimpleValue<T> : ValueBase<T, SimpleValue<T>>
    {
        protected  T _value;
        public SimpleValue()
        {
            _value = default(T);
        }
        public SimpleValue(T value)
        {
            Value = value;
        }

        public SimpleValue(SimpleValue<T> v)
        {
            Value = v._value;
        }
        public static implicit operator T(SimpleValue<T> potentialValue)
        {
            return potentialValue.Value;
        }

        public static implicit operator SimpleValue<T>(T value)
        {
            SimpleValue<T> v = new SimpleValue<T>();
            v.Value = value;
            return v;
        }

        public override T Value { get { return _value; }
                                 set { _value = value; } }
        
    }
}

