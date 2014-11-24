using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using MFramework.Common.Core.Extensions;

namespace MFramework.Common.Core.Serializers.Memento
{
    public abstract class MementoHolderBase<TOriginator> : IMementoHolder
        where TOriginator : class,IMementoOriginator<ExpandoObject>, new()
    {
        protected Func<TOriginator, TOriginator> _createSetter;
        protected Func<dynamic, ExpandoObject> _loadSetter;

        protected MementoHolderBase()
            : this(o => o, o => o)
        {

        }
        protected MementoHolderBase(Func<dynamic, ExpandoObject> loadSetter):this(loadSetter,o=>o)
        {
            
        }

        protected MementoHolderBase(Func<dynamic, ExpandoObject> loadSetter, Func<TOriginator, TOriginator> createSetter)
        {
            _createSetter = createSetter;
            _loadSetter = loadSetter;
        }
        
        public TOriginator GetObject(bool create)
        {
            return create ? CreateObject() : RestoreObject();
        }
        public TOriginator CreateObject()
        {
            return CreateObject(_createSetter);
        }
        public TOriginator CreateObject(Func<TOriginator, TOriginator> setter)
        {
            return new TOriginator().With(o =>
            {
                o.RegisterHolder(this);
                return setter(o);
            });
        }
        public TOriginator RestoreObject()
        {
            return RestoreObject(_loadSetter);
        }
        public TOriginator RestoreObject(Func<dynamic,ExpandoObject> setter)
        {
            return new TOriginator().With(o =>
            {

                var m = new MementoSerializableBase();
                CloseStream(m.Deserialize(GetStreamToRead(o)));
                m.State = setter(m.State);
                o.RestoreMemento(m);
                o.RegisterHolder(this);
                return o;
            });
        }
        public void SaveObject(IMementoOriginator<ExpandoObject> o)
        {
            new MementoSerializableBase().Do(m =>
            {
                o.SetMemento(m);
                CloseStream(m.Serialize(GetStreamToWrite(o as TOriginator)));
            });
            
        }
        protected abstract Stream GetStreamToWrite(TOriginator originator );
        protected abstract Stream GetStreamToRead(TOriginator originator);
        protected abstract void CloseStream(Stream stream);
        

        public void SaveObject<T>(IMementoOriginator<T> o)
        {
            SaveObject(o as IMementoOriginator<ExpandoObject>); ;
        }

        public IMementoOriginator<T> Associate<T>(IMementoOriginator<T> o)
        {

            return (IMementoOriginator<T>) Associate(o as IMementoOriginator<ExpandoObject>);
        }
        public TOriginator Associate(IMementoOriginator<ExpandoObject> o)
        {
            o.RegisterHolder(this);
            return (TOriginator) o;
        }
    }


    public interface IMementoHolder
    {
        void SaveObject<T>(IMementoOriginator<T> o);
        IMementoOriginator<T> Associate<T>(IMementoOriginator<T> o);
    }

}
