using StateService.Models;

namespace StateService
{
    public interface IStateManager
    {
        State Redo();
        State Undo();
        void UpdateState(State state);
    }
}