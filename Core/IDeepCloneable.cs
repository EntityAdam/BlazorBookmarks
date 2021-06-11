using System.Threading.Tasks;

namespace Core
{
    public interface IDeepCloneable<T>
    {
        public T DeepCopy();
    }
}