using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.ConditionalValues
{
    public interface IValue
    {
        
        bool IsComparableTo(IValue v);
        Type ValueFor();
    }
    public interface IValue<T> : IEquatable<IValue<T>>, IValue 
    {
        T Value { get; set; }
        
        IValue<T> Clone();

        IValue<T> Clone(T value);
    }
}
