using System.Collections.Generic;
using API.Models;
using API.Types;

namespace API.Dtos
{
    public class ChatRoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ChatType Type { get; set; }
        public ICollection<MessageDto> Messages { get; set; }
        public ICollection<UserDto> Users { get; set; }
    }
}