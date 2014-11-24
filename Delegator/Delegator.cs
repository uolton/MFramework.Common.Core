using System;
using System.Collections.Generic;
using System.Linq;
using Fasterflect;
using MFramework.Common.Core.Collections;
using MFramework.Common.Core.Collections.Extensions;
using MFramework.Common.Core.Extensions;
using MFramework.Common.Core.Types;
using MFramework.Common.Core.Types.Extensions;

namespace MFramework.Common.Core.Delegator
{


    public static class Value<T>
    {
        public static NullValue<T> Null
        {
            get { return new NullValue<T>(); }
        }
    }

    public struct NullValue<T>
    {

        public T Value
        {
            get { return default(T); }
        }

    }

    public static class DelegatorType
    {
        public delegate TResult Delegator<out TResult>();

        public delegate TResult UnaryDelegator<in TArg1, out TResult>(TArg1 arg1);

        public delegate TResult BinaryDelegator<in TArg1, in TArg2, out TResult>(TArg1 arg1, TArg2 arg2);

        public delegate TResult TernaryDelegator<in TArg1, in TArg2, in TArg3, out TResult>(
            TArg1 arg1, TArg2 arg2, TArg3 arg3);
    }

    public interface IDelegatorArgumentAccessor<TDelegator> where TDelegator : class
    {
        T ArgumentGet<T>(int index);
        TDelegator ArgumentSet<T>(int index, T value);
    }

    public class ArgumentBinder<T> : IListTypeAdapter
    {
        private T _bindValue;
        private bool _hasBeenSet;

        public ArgumentBinder()
        {
            _hasBeenSet = false;
        }

        public ArgumentBinder(T value)
            : this()
        {
            BindValue(value);
        }


        public T BindValue(T value)
        {
            _bindValue = value;
            _hasBeenSet = true;
            return _bindValue;

        }

        public T1 BindedValue<T1>()
        {
            return (dynamic) _bindValue;

        }

        public T1 Bind<T1>(T1 value)
        {
            return _hasBeenSet ? (dynamic) _bindValue : value;
        }

        public T1 Bind<T1>(NullValue<T1> value)
        {
            return Bind(value.Value);
        }

        public T1 ValueGet<T1>()
        {
            return BindedValue<T1>();
        }

        public T1 ValueSet<T1>(T1 value)
        {
            return BindValue((dynamic) value);
        }
    }

    public abstract class DelegatorBase<TDelegator> : CloneableTypeBase<TDelegator>,
        IDelegatorArgumentAccessor<TDelegator> where TDelegator : DelegatorBase<TDelegator>
    {
        protected List<IListTypeAdapter> _args;

        protected DelegatorBase(DelegatorBase<TDelegator> t) : this()
        {
            t._args.Copy(_args);
        }

        protected DelegatorBase()
        {
            _args = new List<IListTypeAdapter>();
        }

        protected DelegatorBase(IEnumerable<Type> argsType) : this()
        {
            argsType.ToList().Do(ConstructParameterListsFromType);
        }

        private void ConstructParameterListsFromType(List<Type> l)
        {
            l.ForEach(
                t =>
                    _args.Add((IListTypeAdapter) typeof (ArgumentBinder<>).CloseGenericWith(new[] {t}).CreateInstance()));
        }

        public T ArgumentGet<T>(int index)
        {
            return _args[index - 1].ValueGet<T>();
        }

        public TDelegator ArgumentSet<T>(int index, T value)
        {
            _args[index - 1].ValueSet(value);
            return (TDelegator) this;
        }

        /// <summary>
        /// Clona l'oggetto invocando il copy constructor
        /// </summary>
        /// <returns></returns>
        public override TDelegator Clone()
        {
            return ((TDelegator) typeof (TDelegator).CreateInstance((TDelegator) this));
        }
    }

    public class DelegatorAction : DelegatorBase<DelegatorAction>, IDelegatorAction
    {
        private Action _func;

        public DelegatorAction(Action func)
            : base()
        {
            _func = func;
        }

        public DelegatorAction(DelegatorAction d) : base(d)
        {
            _func = d._func;
        }
        public Action Delegate
        {
            get { return Call; }
        }

        public void Call()
        {
            _func();
        }
    }
    public class DelegatorAction<TArg1> : DelegatorBase<DelegatorAction<TArg1>>, IDelegatorAction<TArg1>
    {
        private Action<TArg1> _func;

        public DelegatorAction(Action<TArg1> func)
            : base(new [] {typeof(TArg1)})
        {
            _func = func;
        }
        public DelegatorAction(DelegatorAction<TArg1> d):base(d)
        {
            _func = d._func;
        }
        public Action<TArg1> Delegate
        {
            get { return Call; }
        }

        public void Call(TArg1 arg =default(TArg1))
        {
            _func(arg);
        }
    }
    public class DelegatorAction<TArg1,TArg2> : DelegatorBase<DelegatorAction<TArg1,TArg2>>, IDelegatorAction<TArg1,TArg2>
    {
        private Action<TArg1,TArg2> _func;

        public DelegatorAction(Action<TArg1,TArg2> func)
            : base(new[] { typeof(TArg1),typeof(TArg2) })
        {
            _func = func;
        }
        public DelegatorAction(DelegatorAction<TArg1,TArg2> d)
            : base(d)
        {
            _func = d._func;
        }
        public Action<TArg1,TArg2> Delegate
        {
            get { return Call; }
        }

        public void Call(TArg1 arg1 = default(TArg1), TArg2 arg2 = default(TArg2))
        {
            _func(_args[0].As<ArgumentBinder<TArg1>>().Bind(arg1),
                _args[1].As<ArgumentBinder<TArg2>>().Bind(arg2)
                );
        }

        public void Call(NullValue<TArg1> arg1, TArg2 arg2)
        {
            _func(_args[0].As<ArgumentBinder<TArg1>>().Bind(arg1),
                _args[1].As<ArgumentBinder<TArg2>>().Bind(arg2)
                );
        }
    }

    public class DelegatorAction<TArg1, TArg2,TArg3> : DelegatorBase<DelegatorAction<TArg1, TArg2,TArg3>>, IDelegatorAction<TArg1, TArg2,TArg3>
    {
        private Action<TArg1, TArg2,TArg3> _func;

        public DelegatorAction(Action<TArg1, TArg2,TArg3> func)
            : base(new[] { typeof(TArg1), typeof(TArg2),typeof(TArg3) })
        {
            _func = func;
        }
        public DelegatorAction(DelegatorAction<TArg1, TArg2,TArg3> d)
            : base(d)
        {
            _func = d._func;
        }
        public Action<TArg1, TArg2,TArg3> Delegate
        {
            get { return Call; }
        }

        public void Call(NullValue<TArg1> arg1, NullValue<TArg2> arg2, TArg3 arg3)
        {
            _func(_args[0].As<ArgumentBinder<TArg1>>().Bind(arg1),
                _args[1].As<ArgumentBinder<TArg2>>().Bind(arg2),
                _args[2].As<ArgumentBinder<TArg1>>().Bind(arg3));

        }

        public void Call(NullValue<TArg1> arg1, TArg2 arg2, NullValue<TArg3> arg3)
        {
            _func(_args[0].As<ArgumentBinder<TArg1>>().Bind(arg1),
                _args[1].As<ArgumentBinder<TArg2>>().Bind(arg2),
                _args[2].As<ArgumentBinder<TArg1>>().Bind(arg3));

        }

        public void  Call(NullValue<TArg1> arg1, TArg2 arg2, TArg3 arg3)
        {
            _func(_args[0].As<ArgumentBinder<TArg1>>().Bind(arg1),
                _args[1].As<ArgumentBinder<TArg2>>().Bind(arg2),
                _args[2].As<ArgumentBinder<TArg1>>().Bind(arg3));

        }

        public void Call(TArg1 arg1 = default(TArg1), TArg2 arg2 = default(TArg2), TArg3 arg3 = default(TArg3))
        {
            _func(_args[0].As<ArgumentBinder<TArg1>>().Bind(arg1),
                _args[1].As<ArgumentBinder<TArg2>>().Bind(arg2),
                _args[2].As<ArgumentBinder<TArg1>>().Bind(arg3));

        }
    }
    public class DelegatorFunc<TReturn> : DelegatorBase<DelegatorFunc<TReturn>>, IDelegatorFunction<TReturn>
    {
        private Func<TReturn> _func;

        public DelegatorFunc(Func<TReturn> func) : base()
        {
            _func = func;
        }

        protected DelegatorFunc(DelegatorFunc<TReturn> d) : base(d)

        {
            _func = d._func;
        }

        public Func<TReturn> Delegate
        {
            get { return Call; }
        }

        public TReturn Call()
        {
            return (_func());
        }
    }

    public class DelegatorFunc<TReturn, TArg1> : DelegatorBase<DelegatorFunc<TReturn, TArg1>>,
        IDelegatorFunction<TReturn, TArg1>
    {
        private Func<TArg1, TReturn> _func;

        public DelegatorFunc(Func<TArg1, TReturn> func) : base(new[] {typeof (TArg1)})
        {
            _func = func;
        }

        protected DelegatorFunc(DelegatorFunc<TReturn, TArg1> d)
            : base(d)
        {
            _func = d._func;
        }

        public Func<TArg1, TReturn> Delegate
        {
            get { return Call; }
        }

        public TReturn Call(TArg1 arg1 = default(TArg1))
        {
            return (_func(_args[0].As<ArgumentBinder<TArg1>>().Bind(arg1)));
        }

    }

    public class DelegatorFunc<TReturn, TArg1, TArg2> : DelegatorBase<DelegatorFunc<TReturn, TArg1, TArg2>>,
        IDelegatorFunction<TReturn, TArg1, TArg2>
    {
        private Func<TArg1, TArg2, TReturn> _func;

        public DelegatorFunc(Func<TArg1, TArg2, TReturn> func) : base(new[] {typeof (TArg1), typeof (TArg2)})
        {
            _func = func;
        }

        protected DelegatorFunc(DelegatorFunc<TReturn, TArg1, TArg2> d)
            : base(d)
        {
            _func = d._func;
        }

        public Func<TArg1, TArg2, TReturn> Delegate
        {
            get { return Call; }
        }

        public TReturn Call(TArg1 arg1 = default(TArg1), TArg2 arg2 = default(TArg2))
        {
            return (_func(_args[0].As<ArgumentBinder<TArg1>>().Bind(arg1),
                _args[1].As<ArgumentBinder<TArg2>>().Bind(arg2)
                ));
        }

        public TReturn Call(NullValue<TArg1> arg1, TArg2 arg2)
        {
            return (_func(_args[0].As<ArgumentBinder<TArg1>>().Bind(arg1),
                _args[1].As<ArgumentBinder<TArg2>>().Bind(arg2)
                ));
        }
    }

    public class DelegatorFunc<TReturn, TArg1, TArg2, TArg3> :
        DelegatorBase<DelegatorFunc<TReturn, TArg1, TArg2, TArg3>>, IDelegatorFunction<TReturn, TArg1, TArg2, TArg3>
    {
        private Func<TArg1, TArg2, TArg3, TReturn> _func;

        public DelegatorFunc(Func<TArg1, TArg2, TArg3, TReturn> func)
            : base(new[] {typeof (TArg1), typeof (TArg2), typeof (TArg3)})
        {
            _func = func;
        }

        protected DelegatorFunc(DelegatorFunc<TReturn, TArg1, TArg2, TArg3> d)
            : base(d)
        {
            _func = d._func;
        }

        public Func<TArg1, TArg2, TArg3, TReturn> Delegate
        {
            get { return Call; }
        }

        public TReturn Call(NullValue<TArg1> arg1, NullValue<TArg2> arg2, TArg3 arg3)
        {
            return (_func(_args[0].As<ArgumentBinder<TArg1>>().Bind(arg1),
                _args[1].As<ArgumentBinder<TArg2>>().Bind(arg2),
                _args[2].As<ArgumentBinder<TArg1>>().Bind(arg3)));

        }

        public TReturn Call(NullValue<TArg1> arg1, TArg2 arg2, NullValue<TArg3> arg3)
        {
            return (_func(_args[0].As<ArgumentBinder<TArg1>>().Bind(arg1),
                _args[1].As<ArgumentBinder<TArg2>>().Bind(arg2),
                _args[2].As<ArgumentBinder<TArg1>>().Bind(arg3)));

        }

        public TReturn Call(NullValue<TArg1> arg1, TArg2 arg2, TArg3 arg3)
        {
            return (_func(_args[0].As<ArgumentBinder<TArg1>>().Bind(arg1),
                _args[1].As<ArgumentBinder<TArg2>>().Bind(arg2),
                _args[2].As<ArgumentBinder<TArg1>>().Bind(arg3)));

        }

        public TReturn Call(TArg1 arg1 = default(TArg1), TArg2 arg2 = default(TArg2), TArg3 arg3 = default(TArg3))
        {
            return (_func(_args[0].As<ArgumentBinder<TArg1>>().Bind(arg1),
                _args[1].As<ArgumentBinder<TArg2>>().Bind(arg2),
                _args[2].As<ArgumentBinder<TArg1>>().Bind(arg3)));

        }

    }

    public static class ActionExtension
    {
        public static DelegatorAction ToDelegator(this Action @this)
        {
            return new DelegatorAction(@this);
        }
        public static DelegatorAction<TArg1> ToDelegator<TArg1>(this Action<TArg1> @this)
        {
            return new DelegatorAction<TArg1>(@this);
        }
        public static DelegatorAction<TArg1, TArg2> ToDelegator<TArg1,TArg2>(this Action<TArg1,TArg2> @this)
        {
            return new DelegatorAction<TArg1,TArg2>(@this);
        }
        public static DelegatorAction<TArg1, TArg2, TArg3> ToDelegator<TArg1, TArg2, TArg3>(this Action<TArg1, TArg2,TArg3> @this)
        {
            return new DelegatorAction<TArg1, TArg2,TArg3>(@this);
        }
        
    }
    public static class FuncExtension
    {
        public static DelegatorFunc<TReturn, TArg1, TArg2, TArg3> ToDelegator<TArg1, TArg2, TArg3, TReturn>(this Func<TArg1, TArg2, TArg3, TReturn> @this)
        {
            return new DelegatorFunc<TReturn, TArg1, TArg2, TArg3>(@this);
        }
        public static DelegatorFunc<TReturn, TArg1, TArg2> ToDelegator<TArg1, TArg2, TReturn>(this Func<TArg1, TArg2, TReturn> @this)
        {
            return new DelegatorFunc<TReturn, TArg1, TArg2>(@this);
        }
        public static DelegatorFunc<TReturn, TArg1> ToDelegator<TArg1, TReturn>(this Func<TArg1, TReturn> @this)
        {
            return new DelegatorFunc<TReturn, TArg1>(@this);
        }
        public static DelegatorFunc<TReturn> ToDelegator<TReturn>(this Func<TReturn> @this)
        {
            return new DelegatorFunc<TReturn>(@this);
        }
    }

}
