namespace Core.Models
{
    public record BookmarkQueryModel(int Id, int FolderId, string Url, string Name, int Clicks, bool HasChangesToPersist);
}