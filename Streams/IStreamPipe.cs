using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Streams
{
    public interface IStreamPipe
    {
        void Bind(IStreamFilterRoleWriter writer);
        void Bind(IStreamFilterRoleReader reader);


    }
}
