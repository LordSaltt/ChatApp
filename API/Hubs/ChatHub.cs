using System.Threading.Tasks;
using API.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessage(MessageDto messageDto) {
           return Clients.All.SendAsync("ReceiveMessage", messageDto) ;
        }
    }
}