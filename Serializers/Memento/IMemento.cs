using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Serializers.Memento
{
    public interface IMemento<T>
    {
        T State { get; set; }
        void SaveState(Func<dynamic, T> saveAction);
        void SaveState(Func<T, T> saveAction);
        void RestoreState(Action<dynamic> restoreAction);
        void RestoreState(Action<T> restoreAction);
    }
}
