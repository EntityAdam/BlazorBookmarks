using Core.Models;
using HtmlAgilityPack;
using System.IO;

namespace Core
{
    public static class EdgeBookmarkParser
    {
        public static (IEnumerable<FolderModel>, IEnumerable<BookmarkModel>) ReadFile(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var doc = new HtmlDocument();
            doc.Load(stream);
            return ReadDoc(doc);
        }

        public static (IEnumerable<FolderModel>, IEnumerable<BookmarkModel>) ReadFile(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException();
            var doc = new HtmlDocument();
            doc.Load(filename);
            return ReadDoc(doc);
        }

        public static (IEnumerable<FolderModel>, IEnumerable<BookmarkModel>) ReadDoc(HtmlDocument htmlDocument)
        {
            var bookmarkExportFolderNodes = htmlDocument.DocumentNode.SelectNodes("//h3");
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
                var folder = new FolderModel(folderIndex, folderName, lastModifiedUtcDateTime);
                folders.Add(folder);
            }
            var bookmarkNodes = htmlDocument.DocumentNode.SelectNodes("//a");
            foreach (var bookmarkNode in bookmarkNodes)
            {
                var bookMarkurl = bookmarkNode.Attributes["HREF"].Value;
                var bookMarkaddDate = bookmarkNode.Attributes["ADD_DATE"].Value;
                var bookMarkName = bookmarkNode.InnerText;
                /* TODO: Fix Id 0, FolderId 0*/
                var bookmark = new BookmarkModel(Id: 0, FolderId: 0, bookMarkurl, bookMarkName, Clicks: 0);
                bookmarks.Add(bookmark);
            }
            return (folders, bookmarks);
        }
    }
}
