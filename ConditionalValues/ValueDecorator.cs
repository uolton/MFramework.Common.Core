using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.ConditionalValues
{
    public abstract class ValueDecorator<T, TDecorator> : ValueBase<T, TDecorator> where TDecorator : ValueDecorator<T, TDecorator> ,new()
    {
        protected IValue<T> _value;

        protected ValueDecorator(IValue<T> value)
        {
            _value = value;
        }
        public new virtual  bool IsComparableTo(IValue v)
        {
            return v.IsComparableTo(v);
        }
        public override T Value
        {
            get { return (_value.Value); } 
            set { _value.Value = value; }
        }

        
    }
}
