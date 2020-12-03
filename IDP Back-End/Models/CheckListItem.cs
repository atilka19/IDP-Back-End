using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDP_Back_End.Models
{
    public class CheckListItem
    {
        public int ID { get; set; } // PK
        public int TaskID { get; set; } // Task it belongs to FK
        // Entity FrameWork Stuff
        public Task Task { get; set; }

        // Other Data
        public string Title { get; set; }
        public bool Done { get; set; }
    }
}
