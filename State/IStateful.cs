using StateService.Models;

namespace StateService
{
    public interface IStateful
    {
        public State SaveState();
        public void RestoreState(State state);
    }
}
