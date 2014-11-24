using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using MFramework.Common.Core.Extensions;
using MFramework.Common.Core.Reflection;

namespace MFramework.Common.Core.Types.Extensions
{
    /// <summary>
    ///TypeExtensions Classe di estensione delle funzionalita dell'oggetto Type
    /// </summary>
    public static class TypeExtensions
    {
        public static IGenericTypeCloser CloseGeneric(this Type genericType)
        {
            return new GenericTypeCloser(genericType);
        }
        public static  Type CloseGenericWith(this Type genericType,params Type[] closingTypes)
        {
            IGenericTypeCloser c = genericType.CloseGeneric();
            int i = 0;
            (from ct in closingTypes
                select new {index = ++i, type = ct}).ToList().ForEach(x => c.GenericParm(x.index).CloseTo(x.type));
            return c.GetClosedType();
        }
        public static bool IsSubclassOfRawGeneric(this Type toCheck, Type baseType)
        {
            while (toCheck != null && toCheck != typeof(object) )
            {
                Type cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (baseType == cur)
                {
                    return true;
                }

                toCheck = toCheck.BaseType;
            }

            return false;
        }
        /// <summary>
        /// Implement Metodo che verifica se il tipo implementa la classe o interfaccia
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeImplementer"></param>
        /// <returns></returns>
        public static bool Implement<T>(this Type typeImplementer)
        {
            return(Implement(typeImplementer,typeof(T)));
        }
        /// <summary>
        /// Implement Metodo che verifica se il tipo implementa la classe o interfaccia
        /// </summary>
        /// <param name="typeImplementer">Type soggetto della verifica</param>
        /// <param name="typeToImplment">Classe o interfaccia da implementare</param>
        /// <returns></returns>
        public static bool Implement(this Type typeImplementer, Type typeToImplment)
        {
            return TypeClassification.GetTypeOf(typeToImplment).IsImplementedBy(typeImplementer);
        }
        /// <summary>
        /// Risale al tipo base fintanto che l'espressione non è valida
        /// </summary>
        /// <param name="this"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static Type BaseTypeUntil(this Type @this, Func<Type, bool> condition)
        {
            if ( @this == typeof(object) ) return @this; 
            if (! condition(@this)) return BaseTypeUntil(@this.BaseType, condition);
            return @this;
        }
        
        private abstract class TypeClassification
        {
            protected Type _type;

            protected TypeClassification(Type type)
            {
                _type = type;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            protected virtual bool ItSame(Type t)
            {
                return t.Equals(_type);
            }
            public virtual bool IsImplementedBy(Type t)
            {

                return (ItSame(t) || CheckIfIsImplementedBy(t));
            }

            protected abstract bool CheckIfIsImplementedBy(Type t);
            
            public static TypeClassification GetTypeOf(Type t)
            {
                if (t.IsInterface && t.IsGenericType) return new GenericInterfaceType(t);
                if (t.IsInterface ) return new InterfaceType(t);
                if (t.IsGenericTypeDefinition) return new GenericClassType(t);
                return new ClassType(t);
            }
            private class InterfaceType : TypeClassification
            {
                public InterfaceType(Type type) : base(type) { }
                protected override bool CheckIfIsImplementedBy(Type t)
                {
                    return t.GetInterfaces().Any(x => !x.IsGenericType && x == _type); 
                }
            }
            private class GenericInterfaceType : TypeClassification
            {
                public GenericInterfaceType(Type type) : base(type) { }
                protected override bool CheckIfIsImplementedBy(Type t)
                {
                    return t.GetInterfaces()
                        .Any(x => x.IsGenericType 
                            && x.GetGenericTypeDefinition() == _type.GetGenericTypeDefinition()
                            && !x.GenericTypeArguments.Except(_type.GetGenericTypeDefinition().GenericTypeArguments).Any());
                }
            }
            private class ClassType : TypeClassification
            {
                public ClassType(Type type) : base(type) { }
                protected override bool CheckIfIsImplementedBy(Type t)
                {
                    return t.IsSubclassOf(_type); 
                }
            }
            private class GenericClassType : TypeClassification
            {
                public GenericClassType(Type type) : base(type) { }
                protected override bool CheckIfIsImplementedBy(Type t)
                {
                    return (t.IsSubclassOfRawGeneric(_type.GetGenericTypeDefinition()));
                }
            }    
        }
        
        public static object CloseAndInvokeGenericMethod<T>(this T @this, Expression<Func<T, object>> expression, object[] methodArguments, params Type[] typeArguments)
        {
            var methodInfo = ReflectionHelper.GetMember(expression).As<MethodMember>();
            if (methodInfo.GetGenericArguments().Length != typeArguments.Length)
                throw new ArgumentException(
                    string.Format("The method '{0}' has {1} type argument(s) but {2} type argument(s) were passed. The amounts must be equal.",
                    methodInfo.Name,
                    methodInfo.GetGenericArguments().Length,
                    typeArguments.Length));

            return methodInfo
                .GetGenericMethodDefinition()
                .MakeGenericMethod(typeArguments)
                .Invoke(@this, methodArguments);
        }
        
    }
    
}
