using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Models;

namespace API.Services
{
    public interface IChatRoomService
    {
        Task CreateRoom(CreateRoomDto chatRoom);

        Task<List<ChatRoomDto>> GetRooms();

        Task JoinRoom(JoinChatRoomDto joinChatRoomDto);
        
        Task<ICollection<UserDto>> GetChatRoomMembers(int chatRoomId);
        void DeleteRooms();
        Task<ChatRoomDto> GetRoomById(int chatRoomId);
    }
}