using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Collections.Extensions
{
    static public class EnumerableExtensions
    {
        /// <summary>

        /// Copia tutti gli elementi di una collection su una seconda  collection

        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toPush"></param>
        /// <param name="on"></param>
        /// <returns></returns>
        static public IList<T> PushIn<T>(this IEnumerable<T> toPush, IList<T> on)
        {
            toPush.ToList().ForEach(@on.Add);
            return on;
        }

        /// <summary>
        /// Extension Metod : converte una collection di collection del tipo T  in una collection del tipo T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        static public IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> @this)
        {

            List<T> flat = new List<T>();
            @this.ForEach(list => list.PushIn(flat));
            return flat;
        }
        static public string ToSeparatedValueString<T>(this IEnumerable<T> @this, string separator)
        {
            return  string.Join(separator, @this.Select(e => e.ToString()));
        }

        
    }
    
}
