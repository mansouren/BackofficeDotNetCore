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
    public interface ISingletoneSwitchRepositroy : IRepository<CfgSwitch>
    {
        Task<IEnumerable<SwitchDto>> GetAll(CancellationToken cancellationToken);
        Task<CfgSwitch> GetById(int id, CancellationToken cancellationToken);
        Task AddSwitch(SwitchDto dto, CancellationToken cancellationToken);
        Task UpdateSwitch(SwitchDto dto, int id, CancellationToken cancellationToken);
        Task DeleteSwitch(int id, CancellationToken cancellationToken);
        Task<List<CfgSwitch>> GetByIds(List<int> switchCodes, CancellationToken cancellationToken);
    }
}
