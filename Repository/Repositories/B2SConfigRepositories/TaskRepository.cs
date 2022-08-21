using B2SConfig.Models;
using Dto.B2SConfig.Tasks;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Data.Interfaces;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class TaskRepository : B2SConfigRepository<CfgTask>, ITaskRepository
    {
        private readonly AutoMapper.IMapper mapper;
        private readonly ITaskScheduleRepository taskScheduleRepository;

        public TaskRepository(IB2SConfigDBContext dbContext, AutoMapper.IMapper mapper,ITaskScheduleRepository taskScheduleRepository) : base(dbContext)
        {
            this.mapper = mapper;
            this.taskScheduleRepository = taskScheduleRepository;
        }

        public async Task<int> GenerateID()
        {
            var lst = await DBContext.Database.FetchAsync<CfgTask>();
            int id = 0;
            if (lst.Any())
                id = lst.Max(x => x.ID) + 1;
            return id;
        }

        public async Task AddTask(TaskDto dto, CancellationToken cancellationToken)
        {
            DBContext.Database.BeginTransaction();
            try
            {
                var entity = dto.ToEntity(mapper);
                entity.CreatedOn = DateTime.Now;
                await base.AddAsync(entity, cancellationToken);

                var taskSchaduel = new CfgTaskSchedule
                {
                    TaskID = dto.TaskID,
                    IsRunning = dto.IsRunning,
                    LastTaskResult = dto.LastTaskResult,
                    LastTaskResultDescription = dto.LastTaskResultDescription,
                    NextRunTime = dto.NextRunTime
                };

                await taskScheduleRepository.AddAsync(taskSchaduel, cancellationToken);

                DBContext.Database.CompleteTransaction();
            }
            catch
            {
                DBContext.Database.AbortTransaction();
                throw;
            }

        }

        public async Task<IEnumerable<TaskDto>> GetAll(CancellationToken cancellationToken)
        {
            var taskList = await DBContext.Database.FetchAsync<CfgTask>(cancellationToken);
            var taskSceduelList = await DBContext.Database.FetchAsync<CfgTaskSchedule>(cancellationToken);
            var result = taskList.Join(taskSceduelList, o => o.ID, i => i.TaskID, (o, i) =>

                   new TaskDto
                   {
                       CreatedOn = DateTime.Now,
                       MaxDuration = o.MaxDuration,
                       NodeID = o.NodeID,
                       Param1 = o.Param1,
                       Param2 = o.Param2,
                       Param3 = o.Param3,
                       Param4 = o.Param4,
                       Param5 = o.Param5,
                       Param6 = o.Param6,
                       ID = o.ID,
                       PeriodMs = o.PeriodMs,
                       Runat = o.Runat,
                       TaskModuleID = o.TaskModuleID,
                       Title = o.Title,
                       TaskID = o.ID,
                       IsRunning = i.IsRunning,
                       LastTaskResult = i.LastTaskResult,
                       LastTaskResultDescription = i.LastTaskResultDescription,
                       NextRunTime = i.NextRunTime
                   }).ToList();


            if (!result.Any()) throw new Exception("ListIsEmpty");

            return result;
        }

        public async Task UpdateTask(TaskDto dto, int id, CancellationToken cancellationToken)
        {
            DBContext.Database.BeginTransaction();
            try
            {
                var selectedTask = await DBContext.Database.FirstOrDefaultAsync<CfgTask>(cancellationToken, x => x.ID == id);
                var entity = dto.ToEntity(mapper, selectedTask);
                await base.UpdateAsync(entity, cancellationToken);
                var selectedTaskscaduel = await DBContext.Database.FirstOrDefaultAsync<CfgTaskSchedule>(cancellationToken, x => x.TaskID == id);
                selectedTaskscaduel.TaskID = id;
                selectedTaskscaduel.IsRunning = dto.IsRunning;
                selectedTaskscaduel.LastTaskResult = dto.LastTaskResult;
                selectedTaskscaduel.LastTaskResultDescription = dto.LastTaskResultDescription;
                selectedTaskscaduel.NextRunTime = dto.NextRunTime;
                await taskScheduleRepository.UpdateAsync(selectedTaskscaduel, cancellationToken);

                DBContext.Database.CompleteTransaction();
            }
            catch
            {
                DBContext.Database.AbortTransaction();
                throw;
            }

        }
    }
}
