using Core.Models;

namespace Core
{
    public interface IFacade
    {
        void AddFolder(string folderName);
        void DeleteFolder(int folderId);
        void DeleteBookmark(int bookmarkId);
       
        StateModel GetState();
        StateModel GetStateFromStore();
        StateModel Redo();
        void SaveStateToStore(StateModel state);
        void Snapshot(StateModel state);
        StateModel Undo();
    }
}