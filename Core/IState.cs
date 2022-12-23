using System.Threading.Tasks;

namespace Core
{
    public interface IState<T>
    {
        public T Snapshot();
    }
}