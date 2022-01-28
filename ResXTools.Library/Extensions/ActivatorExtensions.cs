using System;

namespace ResXTools.Library.Extensions
{
    internal static class ActivatorExtensions
    {
        internal static T CreateInstance<T>(params object[] args) => (T)Activator.CreateInstance(typeof(T), args);
    }
}