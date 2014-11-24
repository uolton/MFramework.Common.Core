using System;
using System.Linq.Expressions;

namespace MFramework.Common.Core.Reflection
{
    public interface Accessor
    {
        string FieldName { get; }
        object GetValue(object target);

        Accessor GetChildAccessor<T>(Expression<Func<T, object>> expression);

        string Name { get; }
    }
}