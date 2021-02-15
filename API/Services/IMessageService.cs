using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Models;

namespace API.Services
{
    public interface IMessageService
    {
        Task SendMessage(MessageDto message);

        Task<List<MessageDto>> GetChatRoomMessages(int chatRoomId);
    }
}