using AutoMapper;
using SecretSanta.Models;
using SecretSanta.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.BLL.Configuration
{
    public class AutoMapperConfig
    {
        private static Mapper _mapper;

        public static Mapper Instance
        {
            get
            {
                if (_mapper == null)
                {
                    //Register
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<User, UserModel>()
                            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()))
                            .ForMember(dest => dest.Mail, opt => opt.MapFrom(src => src.Mail.Trim()));
                        cfg.CreateMap<UserModel, User>();
                        cfg.CreateMap<Owner, OwnerModel>()
                            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()));
                        cfg.CreateMap<SecretSantaList, ListModel>()
                            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()));
                        cfg.CreateMap<ListModel, SecretSantaList>();
                    });

                    _mapper = new Mapper(config);
                }
                return _mapper;
            }
        }
    }
}
