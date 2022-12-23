using Core;
using Core.Models;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorBookmarks.Providers
{
    public class BookmarkFileStore : IPersistantStore
    {
        private readonly IHostEnvironment environment;

        public BookmarkFileStore(IHostEnvironment environment)
        {
            this.environment = environment;
        }

        private string GetFilePath() => Path.Combine(environment.ContentRootPath, "file.json");

        private async Task SaveToFile(StateModel state)
        {
            var path = GetFilePath();
            var jsonContent = JsonSerializer.Serialize(state);
            await File.WriteAllTextAsync(path, jsonContent);
        }
        private async Task<StateModel> ReadFromFile()
        {
            var jsonContent = await File.ReadAllTextAsync(GetFilePath());
            return JsonSerializer.Deserialize<StateModel>(jsonContent);
        }

        public async Task Save(StateModel state) => await SaveToFile(state);

        public async Task<StateModel> Get() => await ReadFromFile();
    }
}
