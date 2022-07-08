using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class EdgeBookmarkParserTests
    {
        [Fact]
        public void Test()
        {
            string filename = @"C:\test\multifolder.html";
            var reader = new EdgeBookmarkParser();
            var x = reader.ReadFile(filename);
        }
    }
}
