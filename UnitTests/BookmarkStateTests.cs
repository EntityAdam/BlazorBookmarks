using Core.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class BookmarkStateTests
    {
        [Fact]
        public void Bookmark_Should_ShallowCopy()
        {
            var b1 = new BookmarkModel() { Id = 1, FolderId = 1, Name = "Bookmark1" };

            var b2 = b1.ShallowCopy();

            Assert.True(b1.Name == b2.Name && b1.Id == b2.Id);
            Assert.False(ReferenceEquals(b1, b2));
        }
    }
}
