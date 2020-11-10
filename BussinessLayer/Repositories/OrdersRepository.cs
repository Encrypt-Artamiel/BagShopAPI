using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.Interfaces;
using AutoMapper;
using BussinessLayer.Mapping;

namespace BussinessLayer.Repositories
{
    public class OrdersRepository:RepositoryBase<DataAccessLayer.Order,DTO.Order>,IOrdersRepository
    {
        public OrdersRepository(LNBagShopDBEntities context) : base(context)
        {

        }

        public void addOrder(DTO.Order order)
        {
            var config = new MapperConfiguration(cfg => 
                cfg.AddProfile(new MappingProfile())
            );
            var mapper = config.CreateMapper();
            DataAccessLayer.Order orderEntity =
                mapper.Map<DTO.Order, DataAccessLayer.Order>(order);
            //chuyển từ order thành orderEntity
            try
            {
                _context.Set<DataAccessLayer.Order>().Add(orderEntity);
                //add vào order
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Add failed");
            }
        }
    }
}
