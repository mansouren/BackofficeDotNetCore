using B2SConfig.Models;
using Data;
using Dto.B2SConfig.Switch;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Data.Interfaces;
using Data.Base;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class SwitchRepository : B2SConfigRepository<CfgSwitch>, ISwitchRepository,ISingletoneSwitchRepositroy
    {
        private readonly AutoMapper.IMapper mapper;

        public SwitchRepository(IB2SConfigDBContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task AddSwitch(SwitchDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(mapper);

            var maxSwitchCode =await DBContext.Database.ExecuteScalarAsync<int>(cancellationToken, "SELECT MAX(SwitchCode)+1 FROM B2SConfig.dbo.CfgSwitches");
            if (maxSwitchCode == 0)
                model.SwitchCode = 1;
            else
            model.SwitchCode =Convert.ToInt32(maxSwitchCode) ;
            await base.AddAsync(model, cancellationToken);
        }

        public async Task DeleteSwitch(int id, CancellationToken cancellationToken)
        {
            var entity = await GetById(id, cancellationToken);
            if (entity == null) throw new Exception("ItemNotFound");
            await base.DeleteAsync(entity, cancellationToken);
        }

        public async Task<IEnumerable<SwitchDto>> GetAll(CancellationToken cancellationToken)
        {
            var entities = await DBContext.Database.FetchAsync<CfgSwitch>(cancellationToken);
            return entities.Select(x => new SwitchDto
            {
                ID = x.ID,
                ATMSupport = x.ATMSupport,
                IIN = x.IIN,
                IssuerFITTableID = x.IssuerFITTableID,
                IssuerMonitorSupport = x.IssuerMonitorSupport,
                IVRSupport = x.IVRSupport,
                KIOSKSupport = x.KIOSKSupport,
                MerchantEntityID = x.MerchantEntityID,
                MobileSupport = x.MobileSupport,
                NETSupport = x.NETSupport,
                NodeType = x.NodeType,
                POSSupport = x.POSSupport,
                RemoteAccessPassword = x.RemoteAccessPassword,
                RemoteAccessSupport = x.RemoteAccessSupport,
                SettlementModel = x.SettlementModel,
                SwitchAccountID = x.SwitchAccountID,
                SwitchCode = x.SwitchCode,
                SwitchName = x.SwitchName,
                TerminalEntityID = x.TerminalEntityID,
                TerminalKeyID = x.TerminalKeyID,
                TraceEntityID = x.TraceEntityID,
                WatchOutAddress = x.WatchOutAddress
            });
        }

        public async Task<CfgSwitch> GetById(int id, CancellationToken cancellationToken)
        {
            var entity = await DBContext.Database.FirstOrDefaultAsync<CfgSwitch>(cancellationToken, x => x.ID == id);
            if (entity == null) throw new Exception("ItemNotFound");
            return entity;
        }

        public async Task<List<CfgSwitch>> GetByIds(List<int> switchCodes, CancellationToken cancellationToken)
        {
            try
            {
                if (switchCodes == null || switchCodes.Count == 0)
                    return new List<CfgSwitch>();
                
                var q = base.GetAll(CfgSwitch.Schema);
                q.AND(CfgSwitch.Columns.SwitchCode, BComparison.In, switchCodes);
                var a = await DBContext.Database.FetchAsync<CfgSwitch>(cancellationToken, q.q);
                return a;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
            
        }

        public async Task UpdateSwitch(SwitchDto dto, int id, CancellationToken cancellationToken)
        {
            var model = await GetById(id, cancellationToken);
            model = dto.ToEntity(mapper, model);
            //var maxSwitchCode = await DBContext.Database.ExecuteScalarAsync<int>(cancellationToken, "SELECT MAX(SwitchCode)+1 FROM B2SConfig.dbo.CfgSwitches");
            //if (maxSwitchCode == 0)
            //    model.SwitchCode = 1;
            //else
            //    model.SwitchCode = maxSwitchCode;
            await base.UpdateAsync(model, cancellationToken);
        }
    }
}
