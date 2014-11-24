using System;

namespace MFramework.Common.Core.Delegator
{
    public interface  IDelegatorAction
    {
        Action Delegate { get; }
        void Call();
    }

    public interface IDelegatorAction<TArg1>
    {
        Action<TArg1> Delegate { get; }
        void Call(TArg1 arg = default(TArg1));
    }
    public interface IDelegatorAction<TArg1,TArg2>
    {
        Action<TArg1, TArg2> Delegate { get; }
        void Call(TArg1 arg1 = default(TArg1), TArg2 arg2 = default(TArg2));
        void Call(NullValue<TArg1> arg1, TArg2 arg2);
    }

    public interface IDelegatorAction<TArg1, TArg2,TArg3>
    {
        Action<TArg1, TArg2,TArg3> Delegate { get; }
        void Call(NullValue<TArg1> arg1, NullValue<TArg2> arg2, TArg3 arg3);
        void Call(NullValue<TArg1> arg1, TArg2 arg2, NullValue<TArg3> arg3);
        void Call(NullValue<TArg1> arg1, TArg2 arg2, TArg3 arg3);
        void Call(TArg1 arg1 = default(TArg1), TArg2 arg2 = default(TArg2), TArg3 arg3 = default(TArg3));
    }

    public interface IDelegatorFunction<TReturn>
    {
        Func<TReturn> Delegate { get; }
        TReturn Call();
    }
    public interface IDelegatorFunction<TReturn, TArg1>
    {
        Func<TArg1,TReturn> Delegate { get; }     
        TReturn Call(TArg1 arg =default(TArg1));
    }
    public interface IDelegatorFunction<TReturn, TArg1, TArg2>
    {
        Func< TArg1, TArg2,TReturn> Delegate { get; }
        TReturn Call(TArg1 arg1 = default(TArg1), TArg2 arg2 = default(TArg2));
        TReturn Call(NullValue<TArg1> arg1, TArg2 arg2);
    }
    public interface IDelegatorFunction<TReturn, TArg1, TArg2, TArg3>
    {
        Func<TArg1, TArg2,TArg3,TReturn> Delegate { get; }
        TReturn Call(NullValue<TArg1> arg1, NullValue<TArg2> arg2, TArg3 arg3);
        TReturn Call(NullValue<TArg1> arg1, TArg2 arg2, NullValue<TArg3> arg3);
        TReturn Call(NullValue<TArg1> arg1, TArg2 arg2, TArg3 arg3);
        TReturn Call(TArg1 arg1 = default(TArg1), TArg2 arg2 = default(TArg2), TArg3 arg3 = default(TArg3));
        
    }
}
