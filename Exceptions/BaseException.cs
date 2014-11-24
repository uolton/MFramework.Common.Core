using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Fasterflect;
using MFramework.Common.Core.Collections.Extensions;

namespace MFramework.Common.Core.Exceptions

{
    [Serializable]
    public abstract class BaseException<TException> : BaseException where TException : BaseException<TException>
    {    
        
        protected  BaseException(){}
        protected BaseException(string message): base(message){}

            protected  BaseException(string message,Exception innerException)
                : base(message, innerException){}

        protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            FieldToSerialize()
                .ForEach(fieldInfo =>  
                    this.SetFieldValue(fieldInfo.Name,info.GetValue(fieldInfo.Name,((FieldInfo)fieldInfo).FieldType)));
        }

        public void Serialize(SerializationInfo toInfo)
        {
            FieldToSerialize()
                .ForEach(info => toInfo.AddValue(info.Name, this.GetFieldValue(info.Name), ((FieldInfo)info).FieldType));
        }
        public IEnumerable<MemberInfo> FieldToSerialize()
        {

            return typeof (TException).FieldsWith(Flags.AllMembers,new[] { typeof (ExceptionFieldToSerializeAttribute)});
        }


        public void Serialize(object value, Type ofType, SerializationInfo toInfo,string withName)
        {
            toInfo.AddValue(withName, value, ofType);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            Serialize(info);
        }
    }

        [Serializable]
    public abstract class BaseException: Exception
    {
        protected BaseException() { }
        protected BaseException(string message) : base(message) { }

        protected BaseException(string message, Exception innerException)
            : base(message, innerException) { }

        protected BaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        [AttributeUsage(AttributeTargets.Field)]
        public class ExceptionFieldToSerializeAttribute : Attribute { }

        
        
    }
    
}


