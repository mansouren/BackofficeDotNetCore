using B2SConfig.Models;
using Data;
using Dto.B2SConfig.Connector;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Data.Interfaces;
using Utilities.Exceptions;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class ConnectorBrokerRepository : B2SConfigRepository<CfgConnectorBroker>, IConnectorBrokerRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public ConnectorBrokerRepository(IB2SConfigDBContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task AddConnectorBroker(ConnectorBrokerDto dto, CancellationToken cancellationToken)
        {
            var exist = await DBContext.Database.FirstOrDefaultAsync<CfgConnectorBroker>(cancellationToken, x => x.ConnectorName == dto.ConnectorName);
            if (exist != null) throw new AppException("ConnectorNameIsDuplicated");

            var model = dto.ToEntity(mapper);
            await base.AddAsync(model, cancellationToken);
        }

        public async Task DeleteConnectorBroker(int id, CancellationToken cancellationToken)
        {
            var entity = await GetById(id, cancellationToken);
            if (entity == null) throw new Exception("ItemNotFound");
            await base.DeleteAsync(entity, cancellationToken);
        }

        public async Task<IEnumerable<ConnectorBrokerDto>> GetAll(CancellationToken cancellationToken)
        {
            var list = await DBContext.Database.FetchAsync<CfgConnectorBroker>(cancellationToken);
            return list.Select(c => new ConnectorBrokerDto
            {
                ID = c.ID,
                ValidatorGroupID = c.ValidatorGroupID,
                AdvancedMessagingEnable = c.AdvancedMessagingEnable,
                BrokerID = c.BrokerID,
                ChannelID = c.ChannelID,
                ChannelModuleID = c.ChannelModuleID,
                ConnectionType = c.ConnectionType,
                ConnectorName = c.ConnectorName,
                ConsoleLog = c.ConsoleLog,
                CoreID = c.CoreID,
                Description = c.Description,
                DualPort = c.DualPort,
                EnableLog = c.EnableLog,
                Header = c.Header,
                HeaderLength = c.HeaderLength,
                IsActive = c.IsActive,
                IsPermanent = c.IsPermanent,
                IsPriority = c.IsPriority,
                LenType = c.LenType,
                LocalIP = c.LocalIP,
                LocalPort = c.LocalPort,
                Mode = c.Mode,
                ModuleID = c.ModuleID,
                NodeID = c.NodeID,
                NotifierID = c.NotifierID,
                NotifierName = c.NotifierName,
                PackagerModuleID = c.PackagerModuleID,
                Passshare = c.Passshare,
                PKCS12 = c.PKCS12,
                PosCondition = c.PosCondition,
                PrivateKey = c.PrivateKey,
                PublicKey = c.PublicKey,
                RemoteIP = c.RemoteIP,
                RemotePort = c.RemotePort,
                ReplyHeaderLength = c.ReplyHeaderLength,
                SecurityModel = c.SecurityModel,
                SecurityModuleEnabled = c.SecurityModuleEnabled,
                SourceSwitchCode = c.SourceSwitchCode,
                Timeout = c.Timeout,
                Trailer = c.Trailer,
                TrailerLength = c.TrailerLength


            });
        }

        public async Task<CfgConnectorBroker> GetById(int id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.FirstOrDefaultAsync<CfgConnectorBroker>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new Exception("ItemNotFound");
            return entity;
        }

        public async Task UpdateConnectorBroker(ConnectorBrokerDto dto, int id, CancellationToken cancellationToken)
        {
            var model = await DBContext.Database.FirstOrDefaultAsync<CfgConnectorBroker>(cancellationToken, x => x.ID == id);
            if (model == null) throw new Exception("ItemNotFound");
            if(dto.ConnectorName != model.ConnectorName)
            {
                var exist = await DBContext.Database.FirstOrDefaultAsync<CfgConnectorBroker>(cancellationToken, x => x.ConnectorName == dto.ConnectorName);
                if (exist != null) throw new AppException("ConnectorNameIsDuplicated");
            }
            else
            {
                dto.ID = id;
                model = dto.ToEntity(mapper, model);
                await base.UpdateAsync(model, cancellationToken);
            }  

            


        }
    }
}
