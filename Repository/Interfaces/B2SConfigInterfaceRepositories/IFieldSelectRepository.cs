using B2SConfig.Models;
using Dto.B2SConfig.Entities;
using Repository.PublicInterfaces;


namespace Repository.Interfaces.B2SConfigInterfaceRepositories
{
    public interface IFieldSelectRepository : IRepository<CfgFieldSelect>
    {
        Task<IEnumerable<FieldSelectDto>> GetAll(CancellationToken cancellationToken = default);
        Task<FieldSelectDto> AddFieldSelect(FieldSelectDto fieldSelectDto,CancellationToken cancellationToken);
        Task<FieldSelectDto> UpdateFieldSelect(FieldSelectDto fieldSelectDto,int id,CancellationToken cancellationToken);
    }
}
