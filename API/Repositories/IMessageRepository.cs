using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
          Task<List<Message>> GetChatRoomMessages(int chatRoomId);
    }
}