using SecretSanta.DAL;
using SecretSanta.DAL.Interfaces;
using SecretSanta.DAL.Repositories;
using SecretSanta.BLL.Interfaces;
using SecretSanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.BLL.Configuration;

namespace SecretSanta.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public UserModel ConvertToUser(OwnerModel owner)
        {
            return new UserModel() { Name = owner.Name };
        }

        public List<UserModel> Get(int listID)
        {
            return _unitOfWork.UserRepository.Get(u => u.ListID == listID).Select(u => u.MapTo<UserModel>()).ToList();
        }
        public UserModel Get(string name)
        {
            return _unitOfWork.UserRepository.Get(u => u.Name == name).FirstOrDefault().MapTo<UserModel>();
        }


        public bool ContainsSameUsers(IEnumerable<UserModel> users)
        {
            foreach (var user1 in users)
            {
                foreach (var user2 in users)
                {
                    if (user1.GetHashCode() != user2.GetHashCode() && (user1.Name == user2.Name || user1.Mail == user2.Mail))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool Create(List<UserModel> users, bool save)
        {
            try
            {
                foreach (var user in users)
                {
                    _unitOfWork.UserRepository.Insert(user.MapTo<User>());
                }

                if (save)
                {
                    _unitOfWork.Commit();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int listID, bool save)
        {
            try
            {
                var entities = _unitOfWork.UserRepository.Get(u => u.ListID == listID);

                foreach (var entity in entities)
                {
                    _unitOfWork.UserRepository.Delete(entity);
                }

                if (save)
                {
                    _unitOfWork.Commit();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool Delete(List<User> entities, bool save)
        {
            try
            {
                foreach (var entity in entities)
                {
                    _unitOfWork.UserRepository.Delete(entity);
                }

                if (save)
                {
                    _unitOfWork.Commit();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(List<UserModel> users, bool save)
        {
            try
            {
                List<UserModel> nonCreatedUsers = new List<UserModel>();
                List<User> toDelete = new List<User>();

                //trying to delete usless user data
                int listID = users[0].ListID;
                var sameEntities = _unitOfWork.UserRepository.Get(u => u.ListID == listID).Select(u => u).ToList();
                foreach (var user1 in sameEntities)
                {
                    bool delete = true;
                    foreach (var user2 in users)
                    {
                        if (user1.ID == user2.ID)
                        {
                            delete = false;
                        }
                    }
                    if (delete)
                    {
                        toDelete.Add(user1);
                    }
                }

                if (!Delete(toDelete, true))
                {
                    throw new Exception();
                }

                foreach (var user in users)
                {
                    var userEntity = _unitOfWork.UserRepository.GetByID(user.ID);
                    if (userEntity != null)
                    {
                        //Update
                        userEntity.Mail = user.Mail;
                        userEntity.Name = user.Name;
                        userEntity.Showed = false;
                    }
                    else
                    {
                        //Add To Create
                        nonCreatedUsers.Add(user);
                    }
                }

                if (nonCreatedUsers.Count > 0)
                {
                    //Create
                    Create(nonCreatedUsers, false);
                }

                if (save)
                {
                    _unitOfWork.Commit();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetShowed(UserModel user)
        {
            try
            {
                var entity = _unitOfWork.UserRepository.GetByID(user.ID);
                entity.Showed = true;
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
