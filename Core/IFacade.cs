using System.Threading.Tasks;
using Core.Models;

namespace Core
{
    public interface IFacade
    {
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