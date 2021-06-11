﻿using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Models;

namespace Core
{
    public class BookmarkFileStore : IBookmarkStore
    {
        private async Task SaveToFile(StateModel state)
        {
            var jsonContent = JsonSerializer.Serialize(state);
            await File.WriteAllTextAsync(@"C:\bookmark-test\file.json", jsonContent);
        }
        private async Task<StateModel> ReadFromFile()
        {
            var jsonContent = await File.ReadAllTextAsync(@"C:\bookmark-test\file.json");
            return JsonSerializer.Deserialize<StateModel>(jsonContent);
        }

        public async Task Save(StateModel state) => await SaveToFile(state);

        public async Task<StateModel> Get() => await ReadFromFile();
    }
}
