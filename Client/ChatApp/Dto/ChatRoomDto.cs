using System.Collections.Generic;
using ChatApp.Types;

namespace ChatApp.Dto
{
    public class ChatRoomDto
    {
        public ChatRoomDto()
        {
            Messages = new List<MessageDto>();
            Users = new List<UserDto>();

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ChatType Type { get; set; }
        public List<MessageDto> Messages { get; set; }
        public List<UserDto> Users { get; set; }
    }
}