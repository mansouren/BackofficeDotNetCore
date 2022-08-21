using PetaPoco;
using B2SMain.Models;
using Data.Base;
using Data.Interfaces;
using Repository.Interfaces.B2SMainInterfaceRepositories;
using Repository.PublicClasses;


namespace Repository.Repositories.B2SMainRepositories
{
    public class BasicValueRepository : B2SMainRepository<BasicValue>, IBasicValueRepository
    {
        public BasicValueRepository(IB2SMainDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<BasicValue>> GetByBasicTypeID(int basictypeid)
        {
            try
            {
                var lst =await  DBContext.Database.FetchAsync<BasicValue>(x => x.BasicTypeID == basictypeid);
                
                return lst;
                
            }
            catch
            {

                throw;
            }
        }
    }
}
