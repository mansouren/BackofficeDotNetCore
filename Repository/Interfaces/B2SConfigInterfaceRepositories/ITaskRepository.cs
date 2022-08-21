using B2SConfig.Models;
using Dto.B2SConfig.Tasks;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.B2SConfigInterfaceRepositories
{
    public interface ITaskRepository : IRepository<CfgTask>
    {
        Task<IEnumerable<TaskDto>> GetAll(CancellationToken cancellationToken);
        Task AddTask(TaskDto dto,CancellationToken cancellationToken);
        Task UpdateTask(TaskDto dto,int id,CancellationToken cancellationToken);
    }
}
