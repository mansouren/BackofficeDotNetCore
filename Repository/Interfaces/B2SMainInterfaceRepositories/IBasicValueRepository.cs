using B2SCommon.Models;
using B2SMain.Models;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.B2SMainInterfaceRepositories
{
    public interface IBasicValueRepository : IRepository<BasicValue>
    {
        Task<List<BasicValue>> GetByBasicTypeID(int basictypeid);
    }
}
