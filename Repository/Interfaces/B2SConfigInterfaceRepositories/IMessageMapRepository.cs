using B2SConfig.Models;
using Dto.B2SConfig.Switch;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.B2SConfigInterfaceRepositories
{
    public interface IMessageMapRepository : IRepository<CfgMessageMap>
    {
        Task<IEnumerable<MessageMapDto>> GetAll(CancellationToken cancellationToken);
        Task<MessageMapDto> GetById(int id, CancellationToken cancellationToken);
        Task<MessageMapDto> AddMessageMap(MessageMapDto dto, CancellationToken cancellationToken);
        Task<MessageMapDto> UpdateMessageMap(MessageMapDto dto, int id, CancellationToken cancellationToken);
    }
}
