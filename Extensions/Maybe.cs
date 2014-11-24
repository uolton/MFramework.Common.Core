﻿using System;
using System.Net.Mail;

namespace MFramework.Common.Core.Extensions
{
    public static class Maybe
	{
		public static TResult With<T, TResult>(this T @this, Func<T, TResult> func) where T : class
		{
			if (@this != null)
				return func(@this);
			return default(TResult);
		}

		public static TResult Return<T, TResult>(this T @this, Func<T, TResult> func, TResult @default) where T : class
		{
			if (@this != null)
				return func(@this);
			return @default;
		}

		public static TResult Return<T, TResult>(this T? @this, Func<T, TResult> func, TResult @default) where T : struct
		{
			if (@this.HasValue)
				return func(@this.Value);
			return @default;
		}

		public static TResult With<T, TResult>(this T? @this, Func<T, TResult> func) where T : struct
		{
			if (@this.HasValue)
				return func(@this.Value);
			return default(TResult);
		}

		public static void Do<T>(this T @this, Action<T> func) where T : class
		{
			if (@this != null)
				func(@this);
		}
        
        
		public static void Do<T>(this T? @this, Action<T> func) where T : struct
		{
			if (@this.HasValue)
				func(@this.Value);
		}

        //public static bool WhenTrue(this bool @this, Action act)
        //{
        //    if (@this)
        //    {
        //        act();
        //        return true;
        //    }
        //    return false;
        //}

        public static bool WhenTrue(this bool @this, Func<bool> func)
        {
            if (@this) return func();
            return false;
        }
        public static bool WhenTrue(this bool @this, Action actionToDo)
        {
            if (@this) actionToDo();
            return @this;

        }

        
	}
}