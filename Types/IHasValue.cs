using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Types
{
    public interface IHasValue<T> 
    {
        T Value { get; set; }
            }
}
