namespace Core
{
    public interface IStateManager<T>
    {
        public T CurrentState { get; }

        T Redo();
        T Undo();
        void UpdateState(T state);
    }
}