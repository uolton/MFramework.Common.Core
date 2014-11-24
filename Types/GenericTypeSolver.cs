using System;
using System.Linq;

namespace MFramework.Common.Core.Types
{
    public interface IGenericTypeCloser
    {
        IGenericTypeSetter GenericParm(int index);
        Type GetClosedType();
    }
    public interface IGenericTypeSetter
    {
        IGenericTypeCloser CloseTo(Type t);
    }
    internal class GenericTypeCloser : IGenericTypeSetter,IGenericTypeCloser
    {
        private Type _typeToClose ;
        private int _indexParms;
        private Type[] _args ;
        
        public GenericTypeCloser(Type genericToClose)
        {
            
            _typeToClose = genericToClose;

            _args = new Type[_typeToClose.GetGenericArguments().Count()];
        }
        public IGenericTypeSetter GenericParm(int index)
        {
            
            _indexParms = index;
            return this;
        }

        public IGenericTypeCloser CloseTo(Type t)
        {
            _args[_indexParms -1 ] = t;
            return (this);
        }

        public Type GetClosedType()
        {
            return(_typeToClose.MakeGenericType(_args.ToArray()));
        }        
        
    }
           

    }

