using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; set; }
        IListRepository ListRepository { get; set; }
        IOwnerRepository OwnerRepository { get; set; }

        void Commit();
    }
}
