using StateService.Models;

namespace StateService
{
    public interface ISnapshottable
    {
        State CurrentState { get; }
        StateManager StateManager { get; }

        void Redo();
        void Snapshot();
        void Undo();
    }
}
