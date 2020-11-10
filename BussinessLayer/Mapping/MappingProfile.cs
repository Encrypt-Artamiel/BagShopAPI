using AutoMapper;
using BussinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace BussinessLayer.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<DTO.Product, DataAccessLayer.Product>();
            CreateMap<DataAccessLayer.Product, DTO.Product>();
            //
            CreateMap<DTO.Category, DataAccessLayer.Category>();
            CreateMap<DataAccessLayer.Category, DTO.Category>();
            //
            CreateMap<DTO.Order, DataAccessLayer.Order>();
            CreateMap<DataAccessLayer.Order, DTO.Order>();
            //
            CreateMap<DTO.OrderDetail, DataAccessLayer.OrderDetail>();
            CreateMap<DataAccessLayer.OrderDetail, DTO.OrderDetail>();
        }
    }
}
