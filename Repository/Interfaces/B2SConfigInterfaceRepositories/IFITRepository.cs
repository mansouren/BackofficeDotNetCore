using B2SConfig.Models;
using Dto.B2SConfig;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.B2SConfigInterfaceRepositories
{
    public interface IFITRepository : IRepository<CfgFIT>
    {
        Task<IEnumerable<FITDto>> GetAll(CancellationToken cancellationToken);
        Task<FITDto> GetById(int id, CancellationToken cancellationToken);
        Task<FITDto> AddFIT(FITDto dto, CancellationToken cancellationToken);
        Task<FITDto> UpdateFIT(FITDto dto, int id, CancellationToken cancellationToken);
    }
}
