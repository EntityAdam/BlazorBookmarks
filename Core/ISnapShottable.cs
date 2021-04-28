namespace Core
{
    public interface ISnapshottable<T> where T : IDeepCloneable<T>
    {
        T Redo();
        void Snapshot(T state);
        T Undo();
    }
}
