using System.Threading.Tasks;
using Core.Models;

namespace Core
{
    public interface IPersistantStore
    {
        public Task Save(StateModel state);
        public Task<StateModel> Get();
    }
}