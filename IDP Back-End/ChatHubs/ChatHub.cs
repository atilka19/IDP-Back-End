using IDP_Back_End.Models;
using IDP_Back_End.Repository.Interface;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IDP_Back_End.ChatHubs
{
    public class ChatHub : Hub
    {
        private readonly IChatRepository _repo;

        public ChatHub(IChatRepository repo)
        {
            _repo = repo;
        }
        public async System.Threading.Tasks.Task SendMessage(string userName, string message)
        {
            _repo.SendMessage(userName, message);
            await Clients.All.SendAsync("ReceiveMessage", userName, message);
        }

        public override async System.Threading.Tasks.Task OnConnectedAsync()
        {
            var messageHistory = _repo.GetMessages();

            // Not the most elegant solution, would prefer passing the whole list inside one call
            // Might not have time implement if properly, this solution works
            foreach(ChatMessage msg in messageHistory)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", msg.UserName, msg.Message);
            }
            await base.OnConnectedAsync();
        }

    }
}
