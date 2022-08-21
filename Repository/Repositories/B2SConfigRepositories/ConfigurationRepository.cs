using B2SConfig.Models;
using Data.Interfaces;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;
using Utilities;

namespace Repository.Repositories.B2SConfigRepositories
{
    public class ConfigurationRepository : B2SConfigRepository<Configuration>, IConfigurationRepository
    {
        public ConfigurationRepository(IB2SConfigDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsShaparakCheck()
        {
            try
            {
                var data = await DBContext.Database.FirstOrDefaultAsync<Configuration>(x => x.EngTitle == "Check");
                return data == null ? false : data.Value.ToBoolean();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
