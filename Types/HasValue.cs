using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Types
{
    public abstract class HasValue<T, THasValue>:HasValue<T> where THasValue : HasValue<T, THasValue>,new()
    {
        protected HasValue(){}
        protected HasValue(HasValue<T> other):base(other){}
        protected HasValue(T value):base(value){} 
        public static implicit operator HasValue<T, THasValue>(T value)
        {
            THasValue v = new THasValue();
            v.Value =value;
            return v;
        }

    }


    public class HasValue<T>:IHasValue<T> 
    {
    
        
        protected T _value;

        public HasValue()
        {
            
        } 
        public HasValue(HasValue<T> other)
        {
            _value = other._value;
        } 
        public HasValue(T value)
        {
            _value = value;
        } 
        public virtual T Value { get { return _value; } 
                                 set { _value = value; } 
                                }

        public Reference<T> Reference()
        {
            return new Reference<T>(() =>  Value,value =>Value=value);
            
        }
        public static implicit operator T(HasValue<T> v)
        {
            return v.Value;
        }
        public static implicit operator HasValue<T>(T value)
        {
            return new HasValue<T>(value);
            
        }

    }
}
