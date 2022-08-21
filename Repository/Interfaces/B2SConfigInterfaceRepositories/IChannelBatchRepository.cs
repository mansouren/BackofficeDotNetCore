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
    public interface IChannelBatchRepository : IRepository<CfgChannelBatch>
    {
        Task<IEnumerable<ChannelBatchDto>> GetAllChannels(CancellationToken cancellationToken);
        Task<ChannelBatchDto> GetChannelBatch(long id,CancellationToken cancellationToken);
        Task<ChannelBatchDto> AddChannelBatch(ChannelBatchDto dto,CancellationToken cancellationToken);
        Task<ChannelBatchDto> UpdateChannelBatch(long id,ChannelBatchDto dto,CancellationToken cancellationToken);
    }
}
