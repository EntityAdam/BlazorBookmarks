using System.IO;
using System.Text.Json;
using Core.Models;

namespace Core
{
    public class BookmarkFileStore : IBookmarkStore
    {
        private void SaveToFile(StateModel state)
        {
            var jsonContent = JsonSerializer.Serialize(state);
            File.WriteAllText(@"C:\bookmark-test\file.json", jsonContent);
        }
        private StateModel ReadFromFile()
        {
            var jsonContent = File.ReadAllText(@"C:\bookmark-test\file.json");
            return JsonSerializer.Deserialize<StateModel>(jsonContent);
        }

        public void Save(StateModel state) => SaveToFile(state);

        public StateModel Get() => ReadFromFile();
    }
}
