namespace Core
{
    public interface IStateManager<T>
    {
        public T CurrentState { get; }

        T Redo();
        T Undo();
        void LoadState(T state);
        void Snapshot(T state);
    }
}