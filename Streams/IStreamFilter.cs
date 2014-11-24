using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Streams
{
    public interface IStreamFilter
    {
        void BindTo(IStreamFilter filter);
        
    }
}
