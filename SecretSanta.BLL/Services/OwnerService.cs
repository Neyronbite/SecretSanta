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
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Security.Principal;
using SecretSanta.BLL.Configuration;

namespace SecretSanta.BLL.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OwnerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Login(OwnerModel owner, bool rememberMe)
        {
            //TODO add salt to password, maybe later
            var _owner = Get(owner.Name);

            if (_owner == null)
            {
                return false;
            }
            string password = EncodePassword(owner.Password);

            if (_owner.Password == password)
            {
                string data = JsonConvert.SerializeObject(_owner);
                var ticket = new FormsAuthenticationTicket(1, _owner.Name, DateTime.Now, DateTime.Now.AddHours(1), rememberMe, data);
                var encryptTicket = FormsAuthentication.Encrypt(ticket);
                var aothCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
                HttpContext.Current.Response.Cookies.Add(aothCookie);

                return true;
            }

            return false;
        }

        public bool CheckOwner(OwnerModel owner)
        {
            var _owner = _unitOfWork.OwnerRepository.Get(o => o.Name == owner.Name).FirstOrDefault();

            return _owner == null;
        }

        public bool Register(OwnerModel owner)
        {
            string encodedPass = EncodePassword(owner.Password);
            try
            {
                _unitOfWork.OwnerRepository.Insert(new Owner()
                {
                    Name = owner.Name,
                    Password = encodedPass
                });

                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public OwnerModel Get(string name)
        {
            return _unitOfWork.OwnerRepository.Get(o => o.Name == name).First().MapTo<OwnerModel>();
        }

        private string EncodePassword(string pass)
        {
            string passHash = default;

            using (SHA256 alg = SHA256.Create())
            {
                var arr = alg.ComputeHash(Encoding.UTF8.GetBytes(pass));

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < arr.Length; i++)
                {
                    sb.Append(arr[i].ToString("x2"));
                }
                passHash = sb.ToString();
            }
            return passHash;
        }

        public bool HasSameList(ListModel list)
        {
            var listEntity = _unitOfWork.ListRepository.Get(l => l.Name == list.Name).FirstOrDefault();

            if (listEntity == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string GetClaim(string type)
        {
            var user = HttpContext.Current.User as GenericPrincipal;
            string result = user?.Claims.FirstOrDefault(o => o.Type == type).Value;
            if (result == null)
            {
                return string.Empty;
            }
            return result;

        }
    }
}
