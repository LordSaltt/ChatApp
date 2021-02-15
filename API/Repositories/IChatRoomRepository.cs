using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Repositories
{
    public interface IChatRoomRepository : IRepository<Chat>
    {
        Task<IEnumerable<Chat>> GetChatRooms(); 
        Task<Chat> GetChatRoomById(int chatRoomId);

        void DeleteRooms();     
        
          
    }
}