using System;
using System.Linq;

namespace MFramework.Common.Core.Collections.Extensions
{
    public static class ArrayEstensions
    {

        public static bool Contain<T>(this T[] array, T elem )
        {
            return (array.Any(e => e.Equals(elem)));
        }
        public static bool Any<T>(this T[] array, Func<T, bool> expFunc)
        {
            return(from e in array 
                select e).Any(expFunc);
        }
    }
}
