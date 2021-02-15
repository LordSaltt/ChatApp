using System.Threading.Tasks;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class ChatUserRepository : Repository<ChatUser>, IChatUserRepository
    {
        private readonly DataContext _context;
        public ChatUserRepository(DataContext context) : base(context)
        {
            _context = context;
        }

    }
}