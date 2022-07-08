using Core.Models;
using HtmlAgilityPack;
using System.IO;

namespace Core
{
    public class EdgeBookmarkParser
    {
        public (IEnumerable<FolderModel>, IEnumerable<BookmarkModel>) ReadFile(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException();
            var doc = new HtmlDocument();
            doc.Load(filename);
            var bookmarkExportFolderNodes = doc.DocumentNode.SelectNodes("//h3");
            var folderIndex = -1;
            List<FolderModel> folders = new();
            List<BookmarkModel> bookmarks = new();
            foreach (var folderNode in bookmarkExportFolderNodes)
            {
                folderIndex++;
                var folderName = folderNode.InnerText;
                var addDate = folderNode.Attributes["ADD_DATE"].Value;
                var lastModifiedInEpoch = folderNode.Attributes["LAST_MODIFIED"].Value;
                var lastModifiedUtcDateTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(lastModifiedInEpoch)).UtcDateTime;
                var folder = new FolderModel() { Id = folderIndex, Name = folderName, LastUpdated = lastModifiedUtcDateTime };
                folders.Add(folder);
            }
            var bookmarkNodes = doc.DocumentNode.SelectNodes("//a");
            foreach (var bookmarkNode in bookmarkNodes)
            {
                var bookMarkurl = bookmarkNode.Attributes["HREF"].Value;
                var bookMarkaddDate = bookmarkNode.Attributes["ADD_DATE"].Value;
                var bookMarkName = bookmarkNode.InnerText;
                var bookmark = new BookmarkModel() { Name = bookMarkName, Url = bookMarkurl };
                bookmarks.Add(bookmark);
            }
            return (folders, bookmarks);
        }
    }
}
