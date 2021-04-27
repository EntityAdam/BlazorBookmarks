using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdated { get; set; }

        public bool Hidden { get; set; } // TODO: Requires mapping! I don't want "Hidden" in my core logic

        public Folder ShallowCopy() => (Folder)MemberwiseClone();
    }
}
