using System.Threading.Tasks;

namespace Core
{
    public interface ISnapshottable<T> where T : IState<T>
    {
        Task<T> Redo();
        Task Snapshot(T state);
        Task<T> Undo();
    }
}
