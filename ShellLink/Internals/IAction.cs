namespace ShellLink.Internals
{
    public interface IAction<in T>
    {
        void Invoke(T value);
    }
}