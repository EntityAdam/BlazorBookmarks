using System.Threading.Tasks;
using Core.Models;

namespace Core
{
    public interface IFacade
    {
        Task EditFolderTitle(int folderId, string folderName);
        Task IncrementClicks(int bookmarkId);

        Task EditBookmark(int bookmarkId, string name, string url);
        Task<FolderModel> AddFolder(string folderName);
        Task DeleteFolder(int folderId);
        Task DeleteBookmark(int bookmarkId);
       
        Task<StateModel> GetState();
        Task<StateModel> GetStateFromStore();
        Task<StateModel> Redo();
        Task SaveStateToStore(StateModel state);
        Task Snapshot(StateModel state);
        Task<StateModel> Undo();
    }
}