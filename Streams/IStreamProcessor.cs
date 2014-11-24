using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Streams
{
    public interface IStreamProcessor
    {
        
        IStreamProcessor  Attach(IStreamProcessor p);
        void Flush();
        void Close();
    }
}
