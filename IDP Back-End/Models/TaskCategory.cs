using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDP_Back_End.Models
{
    public class TaskCategory
    {
        public int ID { get; set; } // PK

        // Entity Framework stuff
        public List<Task> Tasks { get; set; }
        public string Title { get; set; }
    }
}
