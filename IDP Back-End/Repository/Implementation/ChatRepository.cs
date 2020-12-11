using IDP_Back_End.Models;
using IDP_Back_End.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDP_Back_End.Repository.Implementation
{
    public class ChatRepository : IChatRepository
    {
        private readonly DBContext _ctx;

        // Dependency Injection
        public ChatRepository(DBContext ctx)
        {
            _ctx = ctx;
        }

        // Returns all the chat messages currently in the DB
        // Ideally we would implemenet a bit of pagination and make the chat infinite scroll.....
        public List<ChatMessage> GetMessages()
        {
            return _ctx.ChatMessages.OrderByDescending(cm => cm.TimePosted).ToList();
        }


        // Saves a new message to DB
        public ChatMessage SendMessage(string username, string message)
        {
            var newMessage = new ChatMessage();
            newMessage.User = _ctx.Users.FirstOrDefault(u => u.UserName == username);
            newMessage.UserName = username;
            newMessage.Message = message;
            newMessage.TimePosted = DateTime.Now;

            _ctx.Attach(newMessage).State = EntityState.Added;
            _ctx.SaveChanges();
            return newMessage;
        }
    }
}
