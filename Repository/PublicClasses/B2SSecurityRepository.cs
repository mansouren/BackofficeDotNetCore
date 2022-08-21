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
    public class B2SSecurityRepository<TEntity> : Repository<TEntity>, IB2SSecurityRepository<TEntity> where TEntity : class
    {
        public B2SSecurityRepository(IB2SSecurityDBContext dbContext) : base(dbContext)
        {
        }
    }
}
