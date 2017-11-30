namespace ShellLink
{
    public interface IAction<in T>
    {
        void Invoke(T value);
    }
}