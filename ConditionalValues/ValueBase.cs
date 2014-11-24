using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFramework.Common.Core.Extensions;

namespace MFramework.Common.Core.ConditionalValues
{
    public abstract class ValueBase<T, TValue> : IValue<T>, IEquatable<ValueBase<T, TValue>> where TValue : ValueBase<T, TValue> ,new()

{

        public static implicit operator ValueBase<T, TValue>(T value)
        {
            ValueBase<T, TValue> v = new TValue();
            v.Value = value;
            return v;
        }

    ///<summary>Returns the value contained in the potential value, throwing a cast exception if the potential value contains no value.</summary>
        public static implicit operator T(ValueBase<T, TValue> potentialValue)
    {
        return potentialValue.Value;
    }

    public static bool operator ==(ValueBase<T,TValue> potentialValue1, ValueBase<T,TValue> potentialValue2)
    {

        return potentialValue1.Equals(potentialValue2);
    }

    ///<summary>Determines if two potential values are not equivalent.</summary>
    public static bool operator !=(ValueBase<T,TValue> potentialValue1, ValueBase<T,TValue> potentialValue2)
    {
        return !potentialValue1.Equals(potentialValue2);
    }

    ///<summary>Determines if two potential values are equivalent.</summary>
    public static bool operator ==(ValueBase<T,TValue> potentialValue1, IValue<T> potentialValue2)
    {
        return potentialValue1.Equals(potentialValue2);
    }

    ///<summary>Determines if two potential values are not equivalent.</summary>
    public static bool operator !=(ValueBase<T,TValue> potentialValue1, IValue<T> potentialValue2)
    {
        return !potentialValue1.Equals(potentialValue2);
    }


    public abstract T Value { get; set; }
        public IValue<T> Clone()
        {
            return Clone(Value);
        }

        public IValue<T> Clone(T value)
        {
            IValue<T> v = Clone();
            v.Value = value;
            return v;
        }


        ///<summary>
    ///Determines if this potential value is equivalent to the given potential value.
    ///Note: All forms of no value are equal, including May.NoValue, May&lt;T&gt;.NoValue, May&lt;AnyOtherT&gt;.NoValue, default(May&lt;T&gt;) and new May&lt;T&gt;().
    ///Note: Null is NOT equivalent to new May&lt;object&gt;(null) and neither is equivalent to new May&lt;string&gt;(null).
    ///</summary>
    public bool Equals(ValueBase<T,TValue> other)
    {
        if (((object) other) == null) return false;
        if (!IsComparableTo(other)) return false;
        if (Value != (other.Value.As<T>())) return false;
        return Equals(Value, other.Value);
    }

    ///<summary>
    ///Determines if this potential value is equivalent to the given potential value.
    ///Note: All forms of no value are equal, including May.NoValue, May&lt;T&gt;.NoValue, May&lt;AnyOtherT&gt;.NoValue, default(May&lt;T&gt;) and new May&lt;T&gt;().
    ///Note: Null is NOT equivalent to new May&lt;object&gt;(null) and neither is equivalent to new May&lt;string&gt;(null).
    ///</summary>
    public bool Equals(IValue<T> other)
    {
        return Equals(other.As<ValueBase<T,TValue>>());
    }

    ///<summary>
    ///Determines if this potential value is equivalent to the given object.
    ///Note: All forms of no value are equal, including May.NoValue, May&lt;T&gt;.NoValue, May&lt;AnyOtherT&gt;.NoValue, default(May&lt;T&gt;) and new May&lt;T&gt;().
    ///Note: Null is NOT equivalent to new May&lt;object&gt;(null) and neither is equivalent to new May&lt;string&gt;(null).
    ///</summary>
    public override bool Equals(object obj)
    {
        return Equals(obj.AsOrNull<ValueBase<T,TValue>>());
    }


    public virtual  bool IsComparableTo(IValue v)
    {
        return typeof (T) == v.ValueFor();
    }

    public virtual Type ValueFor()
    {
        return (typeof (T));
    }
    public override int GetHashCode()
    {
        return ReferenceEquals(Value, null) ? -1 : Value.GetHashCode();
    }
}
}
