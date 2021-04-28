using Core.Models;

namespace Core
{
    public interface IFacade
    {
        StateModel GetState();
        StateModel GetStateFromStore();
        StateModel Redo();
        void SaveStateToStore(StateModel state);
        void Snapshot(StateModel state);
        StateModel Undo();
    }
}