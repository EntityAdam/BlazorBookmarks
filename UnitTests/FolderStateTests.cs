using Core.Models;
using FluentAssertions;
using Xunit;

namespace UnitTests
{
    public class FolderStateTests
    {
        [Fact]
        public void Folder_Should_ShallowCopy()
        {
            var f1 = new FolderModel() { Id = 1, Name = "Folder1" };

            var f2 = f1.ShallowCopy();

            f1.Should().BeEquivalentTo(f2);
            f1.Should().NotBeSameAs(f2);
        }
    }
}
