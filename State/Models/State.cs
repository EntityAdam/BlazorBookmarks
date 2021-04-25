using StateService.Models;
using System;
using System.Collections.Generic;

namespace StateService.Models
{
    public class State : ICloneable
    {
        public State(List<Folder> folders, List<Bookmark> bookmarks)
        {
            this.Folders = folders;
            this.Bookmarks = bookmarks;
        }

        public State(State original)
        {

        }

        public List<Folder> Folders { get; }
        public List<Bookmark> Bookmarks { get; }

        public State Clone()
        {
            
        }
    }
}