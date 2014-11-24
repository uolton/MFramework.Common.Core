using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFramework.Common.Core.Extensions;

namespace MFramework.Common.Core.ConditionalValues
{
    public class ValueDefault<T> : SimpleValue<T>
    {
        private ValueOptional<T> _value;
        private T _defaultTo;

        public ValueDefault():this(default(T))
        {
        }
        public ValueDefault(T @default)
            : this(new ValueOptional<T>(), @default)
        {
        }

        
        
        public ValueDefault(T value,T @default):this(@default)
        {
            _value.Value = value;
        }

        public ValueDefault(ValueOptional<T> value)
            : this(value,default(T)){}
        public ValueDefault(ValueOptional<T> value,T @default)
            
        {
            _defaultTo = @default;
            _value = new ValueOptional<T>(value);
            ;
        }

        public ValueDefault(ValueDefault<T> v)
            : this(v._value,v._defaultTo){}

        public T DefaultValue
        {
            get
            {
                return _defaultTo;

            }
            set { _defaultTo = value; }
        }
        
        public override T Value
        {
            get
            {
                return _value.HasValue?_value.Value:_defaultTo;
            }
            set { _value.Value = value; }
        }
        public bool HasDefaultValue { get { return !_value.HasValue; } }

        
    }
}
