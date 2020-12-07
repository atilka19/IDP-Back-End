using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDP_Back_End.Models
{
    public class Task
    {
        public int ID { get; set; } // PK
        public int CategoryID { get; set; } // Category FK
        public int CreatedByID { get; set; } // User that Created the Task FK
        public int TaskOfID { get; set; } // User that should do the Task FK

        // Entity Framework Stuff
        public List<Comment> Comments { get; set; }
        public List<CheckListItem> CheckListItems { get; set; }
        public User CreatedBy { get; set; }
        public User TaskOf { get; set; }
        public TaskCategory Category { get; set; }

        // All other Data
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumberOfCheckListItems { get; set; }
        public DateTime TimeCreated { get; set; }
        public bool Done { get; set; }
    }
}
