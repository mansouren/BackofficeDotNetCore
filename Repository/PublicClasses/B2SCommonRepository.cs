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
    public class B2SCommonRepository<TEntity> : Repository<TEntity>, IB2SCommonRepository<TEntity> where TEntity : class
    {
        public B2SCommonRepository(IB2SCommonDBContext dbContext) : base(dbContext)
        {
        }
    }
}
