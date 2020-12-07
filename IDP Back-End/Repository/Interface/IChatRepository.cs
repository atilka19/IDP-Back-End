using IDP_Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDP_Back_End.Repository.Interface
{
    public interface IChatRepository
    {
        List<ChatMessage> GetMessages();

        ChatMessage SendMessage(string username, string message);
    }
}
