using System.Collections.Generic;
using System.Linq;

namespace MFramework.Common.Core.Collections.Extensions
{
    static public class ListExtensions
    {
        static public IList<T> PushIn<T>(this IList<T> toPush, IList<T> on)
        {
            toPush.ToList().ForEach(@on.Add);
            return on;
        }
        static public IList<T> Copy<T>(this IList<T> @this, IList<T> to)
        {
            @this.ToList().ForEach(to.Add);
            return @this;
        }
    }
    
    
}
