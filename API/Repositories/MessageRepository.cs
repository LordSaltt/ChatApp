using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly DataContext _context;
        public MessageRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetChatRoomMessages(int chatRoomId)
        {
            var messages = await _context.Messages.Where(c=> c.ChatId == chatRoomId).ToListAsync();
            return messages;
        }
    }
}