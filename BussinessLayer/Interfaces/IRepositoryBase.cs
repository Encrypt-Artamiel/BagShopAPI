using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces
{
    public interface IRepositoryBase<TEntity,TDTO>
        where TEntity : class //TS class nguồn
        where TDTO : class //TD class đến
    {
        TDTO ConvertToDestinationType(TEntity entity);
        TEntity ConvertToSourceType(TDTO entity);
        IEnumerable<TDTO> getAll();
        TDTO getByID(int id);
        IEnumerable<TDTO> Find(Expression<Func<TEntity, bool>>expression);
        void Add(ref TDTO entity);
        void Remove(int id);
    }
}
