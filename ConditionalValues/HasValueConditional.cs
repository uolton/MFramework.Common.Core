using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFramework.Common.Core.Types;

namespace MFramework.Common.Core.ConditionalValues
{
    public class HasValueConditional<T> : HasValue<SimpleValue<T>, HasValueConditional<T>>
    {
        public HasValueConditional() { }
        public HasValueConditional(SimpleValue<T> value) : base(value) { }
        public HasValueConditional(HasValueConditional<T> other) : base(other as HasValue<SimpleValue<T>>) { }

        public new T Value
        {
            get { return base.Value.Value; }
            set { base.Value.Value = value; }
        }
    }
}
