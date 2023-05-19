using System;

namespace SwordAndSoul
{
    public static class FunctionalExtensions
    {
        public static TReturn To<TValue, TReturn>(this TValue value, Func<TValue, TReturn> func)
        {
            return func(value);
        }

        public static TValue To<TValue>(this TValue value, Action<TValue> action)
        {
            action(value);
            return value;
        }

        public static void To<TValue, TReturn>(this TValue value, Func<TValue, TReturn> func, out TReturn @out)
        {
            @out = func(value);
        }

        public static void To<TValue>(this TValue value, Action<TValue> action, out TValue @out)
        {
            action(value);
            @out = value;
        }
    }
}
