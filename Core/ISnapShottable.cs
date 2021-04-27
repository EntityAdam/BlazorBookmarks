namespace Core
{
    public interface ISnapshottable<T> where T : IDeepCloneable<T>
    {
        T CurrentState { get; }
        StateManagerBase<T> StateManager { get; }

        void Redo();
        void Snapshot();
        void Undo();
    }
}
