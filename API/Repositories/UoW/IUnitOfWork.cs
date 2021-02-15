using System.Threading.Tasks;

namespace API.Repositories.UoW
{
    public interface IUnitOfWork
    {
        IAuthRepository Auth {get;}
        IChatRoomRepository ChatRoom {get;}
        IMessageRepository Message {get;}
        IChatUserRepository ChatUser {get;}
        IAuthRepository User {get;}
        Task<int> Complete();
    }
}