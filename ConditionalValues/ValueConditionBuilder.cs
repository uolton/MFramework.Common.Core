using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFramework.Common.Core.Types;

namespace MFramework.Common.Core.ConditionalValues
{
    
    public class ValueConditionBuilder<T> where T:IComparable<T>
    {


        private IHasValue<SimpleValue<T>> _value;
        public ValueConditionBuilder(IHasValue<SimpleValue<T>> value)
        {
            _value=value;
        }

        public ValueConditionBuilder<T> Value(T value)
        {
            _value.Value=new SimpleValue<T>(value);
            return this;
        }
        public ValueConditionBuilder<T> Value()
        {
            _value.Value = new SimpleValue<T>();
            return this;
        }
        public ValueConditionBuilder<T> Optional(T value)
        {
            _value.Value = new ValueOptional<T>(value);
            return this;
        }
        public ValueConditionBuilder<T> Optional()
        {
            _value.Value = new ValueOptional<T>();
            return this;
        }
        public ValueConditionBuilder<T> Default(T defaultValue)
        {

            _value.Value = new ValueDefault<T>(defaultValue);
            return this;
        }

        public ValueConditionBuilder<T> Force(T toValue)
        {

            _value.Value = new ValueForce<T>(_value.Value,toValue);
            return this;
        }
        public ValueConditionBuilder<T> Range(T from, T to)
        {

            _value.Value = new ValueOnRange<T>(_value.Value,from,to);
            return this;
        }
    }
}
