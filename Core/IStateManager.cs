using System.Threading.Tasks;

namespace Core
{
    public interface IStateManager<T>
    {
        Task<T> GetState();
        Task<T> Redo();
        Task<T> Undo();
        Task LoadState(T state);
        Task Snapshot(T state);
    }
}