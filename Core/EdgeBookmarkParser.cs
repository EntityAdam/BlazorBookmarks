using System.IO;

namespace Core
{
    public class EdgeBookmarkParser
    {
        private void ReadFile(string filename)
        {
            if (!Directory.Exists(filename))
                throw new FileNotFoundException();
            var fileContents = File.ReadAllText(filename);
        }
    }    
}