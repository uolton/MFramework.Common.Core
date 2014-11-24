using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using MFramework.Common.Core.Delegator;
using MFramework.Common.Core.Types.Ranges;

namespace MFramework.Common.Core.ConditionalValues
{
    class ValueOnRange<T>:SimpleValue<T> where T:IComparable<T>
    {
        private SimpleValue<T> _value;
        private Range<T> _range;

        public ValueOnRange(T min,T max) : this(new SimpleValue<T>(), new Range<T>(min,max)){}
        public ValueOnRange(T value,T min,T max) : this(new SimpleValue<T>(value), new Range<T>(min,max)){}
        public ValueOnRange(T value) : this(new SimpleValue<T>(value), new Range<T>()){}
        public ValueOnRange() : this(new SimpleValue<T>(), new Range<T>()){}
        public ValueOnRange(SimpleValue<T> value) : this(value, new Range<T>()) { }
        public ValueOnRange(SimpleValue<T> value, Range<T> range)
        {
            if (! range.IsValid) throw new ArgumentException("Intervallo range errato");
            _value = value;
            _range = range;
        }
        public T CurrentValue { get { return _value.Value; } }
        public override T Value {
            get
            {
                if (_range.Min.CompareTo(_value.Value) > 0)  return _range.Min;
                if (_range.Max.CompareTo(_value.Value) < 0)  return _range.Max;
                return _value.Value;
            }
            set { _value.Value = value; } }
    }
}
