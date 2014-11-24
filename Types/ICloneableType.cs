using System;

namespace MFramework.Common.Core.Types
{
    public interface ICloneableType<T>:ICloneable
    {
            new T  Clone();

    }

    public abstract class  CloneableTypeBase<T> : ICloneableType<T>
    {
        public abstract T Clone();
        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
