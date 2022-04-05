using SecretSanta.DAL;
using SecretSanta.DAL.Interfaces;
using SecretSanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.DAL.Repositories
{
    public class ListRepository : GenericRepository<SecretSantaList>, IListRepository
    {
        public ListRepository(SecretSantaEntities context) : base(context)
        {
        }
    }
}
