using SecretSanta.Models;
using SecretSanta.DAL;
using SecretSanta.DAL.Repositories;
using SecretSanta.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.DAL.Interfaces;
using SecretSanta.BLL.Configuration;

namespace SecretSanta.BLL.Services
{
    public class ListService : IListService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(ListModel list, bool save = false)
        {
            try
            {
                _unitOfWork.ListRepository.Insert(list.MapTo<SecretSantaList>());

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

        public int GetListID(ListModel list)
        {
            int id;

            if (list.ID != default)
            {
                id = list.ID;
            }
            else
            {
                id = _unitOfWork.ListRepository.Get(l => l.Name == list.Name).FirstOrDefault().ID;
            }
            return id;
        }

        public List<ListModel> Get(int ownerID)
        {
            return _unitOfWork.ListRepository.Get(l => l.OwnerID == ownerID).Select(l => l.MapTo<ListModel>()).ToList();
        }

        public ListModel GetByID(int listID)
        {
            return _unitOfWork.ListRepository.Get(l => l.ID == listID).First().MapTo<ListModel>();
        }

        public bool Delete(int listID, bool save)
        {
            try
            {
                var entity = _unitOfWork.ListRepository.Get(u => u.ID == listID).First();

                _unitOfWork.ListRepository.Delete(entity);

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

        public bool Update(ListModel list, bool save)
        {
            try
            {
                var listEntity = _unitOfWork.ListRepository.GetByID(list.ID);

                listEntity.Name = list.Name;
                listEntity.Time = list.Time;

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
    }
}
