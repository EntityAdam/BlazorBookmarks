using System.IO;
using System.Text.Json;
using Core.Models;

namespace Core
{
    public class BookmarkFileStore : IBookmarkStore
    {
        private void SaveToFile(State state)
        {
            var jsonContent = JsonSerializer.Serialize(state);
            File.WriteAllText(@"C:\bookmark-test\file.json", jsonContent);
        }
        private State ReadFromFile()
        {
            var jsonContent = File.ReadAllText(@"C:\bookmark-test\file.json");
            return JsonSerializer.Deserialize<State>(jsonContent);
        }

        public void Save(State state) => SaveToFile(state);

        public State Get() => ReadFromFile();
    }
}
