﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PublicInterfaces
{
    public interface IB2SCommonRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
    }
}
