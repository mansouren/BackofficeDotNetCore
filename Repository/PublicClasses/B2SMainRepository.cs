using Data;
using Data.Interfaces;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PublicClasses
{
    public class B2SMainRepository<TEntity> : Repository<TEntity>, IB2SMainRepository<TEntity>
        where TEntity : class
    {
        public B2SMainRepository(IB2SMainDBContext dbContext) : base(dbContext)
        {
        }
    }
}
