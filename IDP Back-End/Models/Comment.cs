using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDP_Back_End.Models
{
    public class Comment
    {
        public int ID { get; set; } // PK
        public int TaskID { get; set; } // Task it Belongs to FK
        public int UserID { get; set; } // User that commented FK

        // Entity Framework Stuff
        public Task Task { get; set; }
        public User User { get; set; }

        // Other Data
        public string Text { get; set; }
        public DateTime TimePosted { get; set; }
    }
}
