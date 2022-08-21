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
    public interface IChannelInfoRepository : IRepository<CfgChannelInfo>
    {
        Task<IEnumerable<ChannelInfoDto>> GetAllChannels(CancellationToken cancellationToken);
        Task<ChannelInfoDto> GetChannelInfo(long id,CancellationToken cancellationToken);
        Task<ChannelInfoDto> AddChannelInfo(ChannelInfoDto dto,CancellationToken cancellationToken);
        Task<ChannelInfoDto> UpdateChannelInfo(ChannelInfoDto dto, long id, CancellationToken cancellationToken);
        
    }
}
