namespace ChatApp.Dto
{
    public class CreateRoomDto
    {
        public string Name { get; set; }
        public bool isPublic { get; set; }
        public int UserId { get; set; }        
    }
}