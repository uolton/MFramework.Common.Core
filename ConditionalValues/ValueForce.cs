using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.ConditionalValues
{
    public class ValueForce<T> : SimpleValue<T>
    {
        private SimpleValue<T> _value;
        private T _forcedValue;

        

        public ValueForce( T forcedValue)
        {
            _value = new SimpleValue<T>();
            _forcedValue = forcedValue;
        }
        public ValueForce(SimpleValue<T> value, T forcedValue)
        {
            _value = value;
            _forcedValue = forcedValue;
        }
        public ValueForce(T value, T fixedValue)
        {
            _value = new SimpleValue<T>(value);
            _forcedValue = fixedValue;
        }
        public T CurrentValue { get { return _value.Value; } }

        public override T Value
        {
            get { return _forcedValue; } 
            set { _value.Value = value; }
        }
    }
}
