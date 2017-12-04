using System;

namespace ShellLink.Internals
{
    public abstract class Error<T>
    {
        public abstract bool Check(T value);

        public abstract IAction<T>[] Fixes { get; }

        public abstract Exception Exception { get; }
    }
}