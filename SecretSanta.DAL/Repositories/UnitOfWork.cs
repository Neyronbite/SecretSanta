using SecretSanta.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public SecretSantaEntities _context { get; set; }

        //****without singltone****
        public IUserRepository UserRepository { get; set; }
        public IListRepository ListRepository { get; set; }
        public IOwnerRepository OwnerRepository { get; set; }

        public UnitOfWork()
        {
            _context = new SecretSantaEntities();

            UserRepository = new UserRepository(_context);
            ListRepository = new ListRepository(_context);
            OwnerRepository = new OwnerRepository(_context);
        }

        #region singltone
        //****with singltone****
        //private IUserRepository _userRepository;
        //private IListRepository _listRepository;
        //private IOwnerRepository _ownerRepository;

        //public IUserRepository UserRepository
        //{
        //    get
        //    {
        //        if (_userRepository == null)
        //        {
        //            _userRepository = new UserRepository(_context);
        //        }
        //        return _userRepository;
        //    }
        //}
        //public IListRepository ListRepository
        //{
        //    get
        //    {
        //        if (_listRepository == null)
        //        {
        //            _listRepository = new ListRepository(_context);
        //        }
        //        return _listRepository;
        //    }
        //}
        //public IOwnerRepository OwnerRepository
        //{
        //    get
        //    {
        //        if (_ownerRepository == null)
        //        {
        //            _ownerRepository = new OwnerRepository(_context);
        //        }
        //        return _ownerRepository;
        //    }
        //}
        #endregion

        public void Commit()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
