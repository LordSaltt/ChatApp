using System.Threading.Tasks;
using API.Data;

namespace API.Repositories.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Auth = new AuthRepository(_context);
            ChatRoom = new ChatRoomRepository(_context);
            Message = new MessageRepository(_context);
            ChatUser = new ChatUserRepository (_context);
            User = new AuthRepository(_context);
        }

        public IAuthRepository Auth { get; private set; }
        public IChatRoomRepository ChatRoom { get; set; }
        public IMessageRepository Message { get; set; }
        public IChatUserRepository ChatUser { get; set; }
        public IAuthRepository User { get; set; }


        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}