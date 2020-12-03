using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDP_Back_End.Models
{
    public class ChatMessage
    {
        public int ID { get; set; } // PK
        public int UserID { get; set; } // User FK

        // Entity FrameWork Stuff
        public User User { get; set; }
        public string Message { get; set; }
    }
}
