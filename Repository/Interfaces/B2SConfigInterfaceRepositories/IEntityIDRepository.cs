using B2SConfig.Models;
using Dto.B2SConfig.Entities;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.B2SConfigInterfaceRepositories
{
    public interface IEntityIDRepository : IRepository<CfgEntityID>
    {
        Task<IEnumerable<EntityIDDto>> GetAll(CancellationToken cancellationToken);
        Task<EntityIDDto> GetByID(int id,CancellationToken cancellationToken);
        Task<EntityIDDto> AddEntityID(EntityIDDto dto, CancellationToken cancellationToken);
        Task<EntityIDDto> UpdateEntityID(EntityIDDto dto, int id,CancellationToken cancellationToken);
    }
}
