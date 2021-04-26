using StateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class State : IDeepCloneable<State>
    {
        public List<Folder> Folders { get; set; }
        public List<Bookmark> Bookmarks { get; set; }

        public State DeepCopy()
        {
            throw new NotImplementedException();
        }
    }
}
