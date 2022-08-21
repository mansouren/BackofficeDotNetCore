using AutoMapper;
using B2SConfig.Models;
using Data;
using Data.Interfaces;
using Dto.B2SConfig.Entities;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class FieldSelectRepository : B2SConfigRepository<CfgFieldSelect>, IFieldSelectRepository
    {
        private readonly AutoMapper.IMapper mapper;

        public FieldSelectRepository(IB2SConfigDBContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

      

        public async Task<IEnumerable<FieldSelectDto>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var list = await DBContext.Database.FetchAsync<CfgFieldSelect>(cancellationToken);
                return list.Select(x => new FieldSelectDto
                {
                    ID = x.ID,
                    AllowMac = x.AllowMac,
                    ChannelIdentifier = x.ChannelIdentifier,
                    FieldMap = x.FieldMap,
                    GroupID = x.GroupId,
                    Mti = x.Mti,
                    PrCode = x.PrCode,
                });

            }
            catch (Exception)
            {
                throw new NullReferenceException("ObjectNotFetched");
            }
        }

        public async Task<FieldSelectDto> AddFieldSelect(FieldSelectDto fieldSelectDto, CancellationToken cancellationToken)
        {
            try
            {
                var entity = fieldSelectDto.ToEntity(mapper);
                await base.AddAsync(entity, cancellationToken);
                return fieldSelectDto;
            }
            catch (Exception)
            {

                throw new Exception("ItemAdditionWasNotsuccessFully");
            }


        }

        public async Task<FieldSelectDto> UpdateFieldSelect(FieldSelectDto fieldSelectDto, int id, CancellationToken cancellationToken)
        {
            try
            {
                var model = await DBContext.Database.FirstOrDefaultAsync<CfgFieldSelect>(cancellationToken,x => x.ID == id);
                model = fieldSelectDto.ToEntity(mapper, model);
                await base.UpdateAsync(model, cancellationToken);
                return fieldSelectDto;
            }
            catch (Exception)
            {

                throw new Exception("ItemEditionWasNotsuccessFully");
            }
        }
    }
}
