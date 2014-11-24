using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Serializers.Memento
{
    public interface IMementoOriginator<T>:IDisposable
    {
        void RegisterHolder(IMementoHolder holder) ;
        void SetMemento(IMemento<T> memento);
        void RestoreMemento(IMemento<T> memento);
    }
}
