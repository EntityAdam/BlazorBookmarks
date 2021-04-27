using Core.Models;

namespace Core
{
    public interface IFacade
    {
        State CurrentState { get; set; }

        void Redo();
        void Snapshot();
        void Undo();
    }
}