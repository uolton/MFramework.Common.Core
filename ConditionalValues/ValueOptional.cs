using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strilanc.Value;

namespace MFramework.Common.Core.ConditionalValues
{
    public class ValueOptional<T>:SimpleValue<T>
    {
        
        private bool _hasValue=false;
        public ValueOptional()
            : base()
        {
            _hasValue = false;
        }
        public ValueOptional(T value) : base(value)
        {
            _hasValue = true;
        }
        public ValueOptional(ValueOptional<T> v)
            : base(v._value)
        {
            _hasValue = v._hasValue;
        }

        public ValueOptional(SimpleValue<T> v)
            : base(v)
        {
            _hasValue = true;
        }
        public bool HasValue { get { return _hasValue; } }
        public override T Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;
                _hasValue = true;
            }
        }
        public static implicit operator ValueOptional<T>(T value)
        {
            return new ValueOptional<T>(value);
        }

        
        ///<summary>Returns the value contained in the potential value, throwing a cast exception if the potential value contains no value.</summary>
        public static implicit operator T(ValueOptional<T> potentialValue)
        {
            return potentialValue._value;
        }
        
    }
}
