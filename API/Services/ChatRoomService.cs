using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Models;
using API.Repositories.UoW;
using API.Types;
using AutoMapper;

namespace API.Services
{
    public class ChatRoomService : IChatRoomService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ChatRoomService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;

        }
        public async Task CreateRoom(CreateRoomDto chatRoom)
        {

            var chatRoomType = chatRoom.isPublic ? ChatType.Room : ChatType.Private;
            var itemToSave = new Chat
            {
                Name = chatRoom.Name,
                Type = chatRoomType,
                Users = new List<ChatUser>()
            };

            itemToSave.Users.Add(new ChatUser
            {
                UserId = chatRoom.UserId,
                Role = UserRole.Admin
            });

            await _uow.ChatRoom.Add(itemToSave);
            await _uow.Complete();
        }

        public void DeleteRooms()
        {
            _uow.ChatRoom.DeleteRooms();
        }

        public async Task<ICollection<UserDto>> GetChatRoomMembers(int chatRoomId)
        {
            var chatUsers = await _uow.ChatUser.GetAll();


            var members = new List<UserDto>();
            foreach (var item in chatUsers)
            {
                members.Add(new UserDto
                {
                    Id = item.UserId
                });

            }
            return members;
        }

        public async Task<ChatRoomDto> GetRoomById(int chatRoomId)
        {
            var chatRoom = await _uow.ChatRoom.GetChatRoomById(chatRoomId);
            var response = new ChatRoomDto
            {
                Id = chatRoom.Id,
                Name = chatRoom.Name,
                Type = chatRoom.Type,
                Users = new List<UserDto>(),
                Messages = new List<MessageDto>()
            };

            foreach (var member in chatRoom.Users)
            {
                response.Users.Add(new UserDto
                {
                    Id = member.UserId,
                    UserName = member.User.UserName
                });
            }

            foreach (var message in chatRoom.Messages)
            {
                response.Messages.Add(new MessageDto
                {
                    Id = message.Id,
                    Name = message.Name,
                    Text = message.Text,
                    Timestamp = message.Timestamp
                });
            }

            return response;
        }

        public async Task<List<ChatRoomDto>> GetRooms()
        {
            var chatRooms = await _uow.ChatRoom.GetChatRooms();


            var list = new List<ChatRoomDto>();
            foreach (var item in chatRooms)
            {
                var user = new ChatRoomDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Type = item.Type,
                    Users = new List<UserDto>(),
                    Messages = new List<MessageDto>()
                };

                foreach (var member in item.Users)
                {
                    user.Users.Add(new UserDto
                    {
                        Id = member.UserId,
                        UserName = member.User.UserName
                    });
                }

                foreach (var message in item.Messages)
                {
                    user.Messages.Add(new MessageDto
                    {
                        Id = message.Id,
                        Name = message.Name,
                        Text = message.Text,
                        Timestamp = message.Timestamp
                    });
                }

                list.Add(user);
            }

            return list;
        }

        public async Task JoinRoom(JoinChatRoomDto joinChatRoomDto)
        {
            //create chat user
            var chatUserJoin = new ChatUser
            {
                UserId = joinChatRoomDto.UserId,
                ChatId = joinChatRoomDto.ChatRoomId
            };

            //Get ChatRoom
            await _uow.ChatUser.Add(chatUserJoin);
            await _uow.Complete();
        }
    }
}