using API.Types;

namespace API.Dtos
{
    public class CreateRoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isPublic { get; set; }
        public int UserId { get; set; }        
    }
}