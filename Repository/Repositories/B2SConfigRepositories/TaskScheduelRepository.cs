using B2SConfig.Models;
using Data;
using Data.Interfaces;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class TaskScheduelRepository : B2SConfigRepository<CfgTaskSchedule>, ITaskScheduleRepository
    {
        public TaskScheduelRepository(IB2SConfigDBContext dbContext) : base(dbContext)
        {
        }
    }
}
