using StateService;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class StateTests
    {
        [Fact]
        public void Test1()
        {
            //create originator
            var x = new Originator();

            //create caretaker
            var careTaker = new StateManager();

            //save state in caretaker
            careTaker.UpdateState(x.SaveState()); //folders 1, bookmarks 1

            x.AddFolder("Test");

            careTaker.UpdateState(x.SaveState()); //folders 2, bookmarks 1

            x.AddFolder("Test");

            var state1 = careTaker.Undo();

            Assert.Equal(2, state1.Bookmarks.Count);

            var state2 = careTaker.Undo();

            Assert.Equal(1, state2.Bookmarks.Count);

            var state3 = careTaker.Undo();

            Assert.Equal(1, state2.Bookmarks.Count);
        }
    }
}
