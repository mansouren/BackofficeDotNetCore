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
    public class B2STxnRepository<TEntity> : Repository<TEntity>, IB2STxnRepository<TEntity>
        where TEntity : class
    {
        public B2STxnRepository(IB2STxnDBContext dbContext) : base(dbContext)
        {
        }
    }
}
