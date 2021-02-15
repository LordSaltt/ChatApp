using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Models;
using API.Repositories.UoW;
using AutoMapper;
using System.Linq;

namespace API.Services
{
    public class MessageService : IMessageService
    {

         private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MessageService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;

        }
        public async Task<List<MessageDto>> GetChatRoomMessages(int chatRoomId)
        {
            var messages = await _uow.Message.GetChatRoomMessages(chatRoomId);
            return messages.Select(x=> _mapper.Map<MessageDto>(x)).ToList();
        }

        public async Task SendMessage(MessageDto message)
        {
            var itemToSave = _mapper.Map<Message>(message);
            await _uow.Message.Add(itemToSave);
            await _uow.Complete();
        }
    }
}