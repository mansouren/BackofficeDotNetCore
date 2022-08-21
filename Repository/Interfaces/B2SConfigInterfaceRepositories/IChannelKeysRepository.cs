using B2SConfig.Models;
using Dto.B2SConfig.Channel;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.B2SConfigInterfaceRepositories
{
    public interface IChannelKeysRepository : IRepository<CfgChannelKey>
    {
        Task<IEnumerable<ChannelKeyDto>> GetAll(CancellationToken cancellationToken);
        Task<ChannelKeyDto> GetById(int id, CancellationToken cancellationToken);
        Task<ChannelKeyDto> AddChannelKey(ChannelKeyDto dto, CancellationToken cancellationToken);

        Task<ChannelKeyDto> UpdateChannelKey(ChannelKeyDto dto, int id, CancellationToken cancellationToken);
       
        
    }
}
