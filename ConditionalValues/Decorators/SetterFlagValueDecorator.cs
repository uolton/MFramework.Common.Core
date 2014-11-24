using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.ConditionalValues.Decorators
{
    class SetterFlagValueDecorator<T> : ValueDecorator<T, SetterFlagValueDecorator<T>>
    {
        private bool _hasBeenSet;

        public SetterFlagValueDecorator(IValue<T> value) : base(value)
        {
            _hasBeenSet = false;
        }

        public SetterFlagValueDecorator(T value) : base(new SimpleValue<T>(value))
        {
            _hasBeenSet = true;
        }

        public SetterFlagValueDecorator() : this((IValue<T>) new SimpleValue<T>()){}
        public override T Value { get
            ; set; }
    }
}
