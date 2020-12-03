using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDP_Back_End.Models
{
    // Should be fine like this, only needs other relations
    public class User
    {
        public int Id { get; set; }
        public bool Admin { get; set; }

        //Entity FrameWork Stuff
        public List<Comment> Comments { get; set; }
        public List<Task> TasksCreated { get; set; }
        public List<Task> TasksOfUser { get; set; }
        public List<ChatMessage> ChatMessages { get; set; }
        public string UserName { get; set; }

        // Hashing Stuff
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}
