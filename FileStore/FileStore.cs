using Core;
using System.IO;
using System.Text.Json;

namespace Storage
{
    public class FileStore : IStore
    {
        public void SaveToFile(State state)
        {
            var jsonContent = JsonSerializer.Serialize(state);
            File.WriteAllText(@"C:\bookmark-test\file.json", jsonContent);
        }
        public State ReadFromFile()
        {
            var jsonContent = File.ReadAllText(@"C:\bookmark-test\file.json");
            return JsonSerializer.Deserialize<State>(jsonContent);
        }
    }
}
