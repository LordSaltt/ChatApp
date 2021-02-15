using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ChatRoomRepository : Repository<Chat>, IChatRoomRepository
    {
        private readonly DataContext _context;
        public ChatRoomRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public void DeleteRooms()
        {
            _context.Chats.RemoveRange(_context.Chats);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Chat>> GetChatRooms()
        {
            var chatRooms = await _context.Chats
                .Include(cr=> cr.Messages)
                .Include(x=> x.Users)
                .ThenInclude(u=> u.User)
                .ToListAsync();
            
            return chatRooms;
        }

        public async Task<Chat> GetChatRoomById(int chatRoomId)
        {
            var chatRoom = await _context.Chats.Where(q=> q.Id==chatRoomId)
                .Include(cr=> cr.Messages)
                .Include(x=> x.Users)
                .ThenInclude(u=> u.User)
                .FirstOrDefaultAsync();            
            return chatRoom;
        }
    }
}