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
    public class B2SConfigRepository<TEntity> : Repository<TEntity>, IB2SConfigRepository<TEntity>
        where TEntity : class
    {
        public B2SConfigRepository(IB2SConfigDBContext dbContext) : base(dbContext)
        {
        }
    }
}
