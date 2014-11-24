using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFramework.Common.Core.Extensions;
using MFramework.Common.Core.Streams;

namespace MFramework.Common.Core.Serializers.Memento
{


    public  class MementoSerializableBase: MementoSerializable<ExpandoObject,DCSerializer,UTF8Encoding>
    
    {
        public  MementoSerializableBase():base()
        {
            
        }
        public MementoSerializableBase(ExpandoObject state)
            : base(state)
        {
        }
        
    }
    public abstract class MementoSerializable<T, TSerializer, TEncoding> : MementoSerializable<T>
        where TSerializer : class, ISerializer, new()
        where TEncoding : Encoding, new()
        where T : class,new()
    {

        protected MementoSerializable():base()
        {
            
        }
        protected MementoSerializable(T state):base(state)
        {
        }
        public Stream Serialize(Stream toStream)

        {
            return Serialize<TSerializer, TEncoding>(toStream);
        }

        public Stream Deserialize(Stream fromStream)

        {
            return Deserialize<TSerializer, TEncoding>(fromStream);
        }

        public override void SaveState(Func<dynamic, T> saveAction)
        {
            dynamic s = new T();
            State = saveAction(s);
        }
        public override void RestoreState(Action<dynamic> restoreAction)
        {
            dynamic s = State;
            restoreAction(s);
        }
        public override void SaveState(Func<T, T> saveAction)
        {
            var s = new T();
            State = saveAction(s);
        }
        public override void RestoreState(Action<T> restoreAction)
        {
            var s = State;
            restoreAction(s);
        }
    }
    
    public abstract class MementoSerializable<T> : IMemento<T>
        where T:class,new()
    {
        

        protected MementoSerializable()
        {
            State = new T();
        }
        protected MementoSerializable(T state)
        {
            State = state;
        }
        public T State { get; set; }
        public abstract void SaveState(Func<dynamic, T> saveAction);
        public abstract void SaveState(Func<T, T> saveAction);
        public abstract void RestoreState(Action<dynamic> restoreAction);
        public abstract void RestoreState(Action<T> restoreAction);

        public Stream Serialize<TSerializer,TEncoding>(Stream toStream) 
            where TSerializer : class, ISerializer, new()
            where TEncoding:Encoding,new()
        {
            var e = new TEncoding();
            e.GetBytes(State.Serialize<TSerializer, T>())
                .Do(b => toStream.Write(b,0,b.Length));
            return toStream;
        }

        public Stream Deserialize<TSerializer, TEncoding>(Stream fromStream)
            where TSerializer : class, ISerializer, new()
            where TEncoding:Encoding,new()
        {
            State = fromStream
                    .AsString()
                    .Deserialize<TSerializer, T>();
            return fromStream;
        }
    }

 
}
