namespace Core
{
    public interface ISnapshottable<T> where T : IDeepCloneable<T>
    {
        T CurrentState { get; set; }

        void Redo();
        void Snapshot();
        void Undo();
    }
}
