using AutoMapper;
using API.Dtos;
using System.Linq;

namespace API.Models.AutoMapperProfiles
{
    public class AutoMapperCustomProfile : Profile
    {
        public AutoMapperCustomProfile()
        {
            CreateMap<Chat,ChatRoomDto>();
            CreateMap<ChatRoomDto, Chat>();

            CreateMap<Message,MessageDto>();
            CreateMap<MessageDto, Message>();

            CreateMap<User,UserDto>();
            CreateMap<UserDto, User>();
        }
        
    }
}