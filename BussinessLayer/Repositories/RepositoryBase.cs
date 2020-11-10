using AutoMapper;
using BussinessLayer.Mapping;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.Interfaces;

namespace BussinessLayer.Repositories
{
    public class RepositoryBase<TEntity,TDTO> : IRepositoryBase<TEntity, TDTO>
        where TEntity : class //table
        where TDTO : class //DTO
    {
        protected readonly LNBagShopDBEntities _context;
        protected IMapper mapper;
        public RepositoryBase(LNBagShopDBEntities context)
        {
            _context = context;
        }
        public TDTO ConvertToDestinationType(TEntity entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            return mapper.Map<TEntity, TDTO>(entity);
            mapper = config.CreateMapper();
        }
        public TD ConvertToDestinationType(TS entity)
        {            
            return mapper.Map<TS, TD>(entity);
        }
        public TEntity ConvertToSourceType(TDTO dto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            return mapper.Map<TDTO, TEntity>(dto);
        }
        public void Add(ref TDTO entity)
        {
            TEntity SourceEntity = ConvertToSourceType(entity);
            _context.Set<TEntity>().Add(SourceEntity);
            _context.SaveChanges();
            entity = ConvertToDestinationType(SourceEntity);
        }

        public IEnumerable<TDTO> Find(Expression<Func<TEntity, bool>> expression)
        {
            var totalResult = _context.Set<TEntity>().Where(expression);
            List<TDTO> returnList = new List<TDTO>();
            foreach(TEntity ts in totalResult)
            {
                returnList.Add(ConvertToDestinationType(ts));
            }
            return returnList;
        }

        public IEnumerable<TDTO> getAll()
        {
            List<TDTO> returnList = new List<TDTO>();
            foreach(TEntity entity in _context.Set<TEntity>().ToList())
            {
                returnList.Add(ConvertToDestinationType(entity));
            }
            return returnList;
        }

        public TDTO getByID(int id)
        {
            return ConvertToDestinationType(_context.Set<TEntity>().Find(id));
        }

        public void Remove(int id)
        {
            _context.Set<TEntity>().Remove(_context.Set<TEntity>().Find(id));
        }
    }
}
