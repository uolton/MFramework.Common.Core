using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Serializers.Memento
{
    public abstract class MementoOriginatorBase:IMementoOriginator<ExpandoObject>
    {
        protected IMementoHolder _holder;
        public void Dispose()
        {
            _holder.SaveObject(this);
        }

        public void RegisterHolder(IMementoHolder holder)
        {
            _holder=holder;
        }

        public void SetMemento(IMemento<ExpandoObject> memento)
        {
            memento.SaveState(SaveState); ;
        }

        public void RestoreMemento(IMemento<ExpandoObject> memento)
        {
            memento.RestoreState(RestoreState);
        }

        protected void HasBeenChanged()
        {
            _holder.SaveObject(this);
        }
        protected abstract ExpandoObject SaveState(dynamic state);
        protected abstract void RestoreState(dynamic state);
    }
}
