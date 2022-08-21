using Data;
using Data.Base;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PublicInterfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        List<TEntity> EntitiesCache { get; }

        public IDBContext DBContext { get; }

        void Add(TEntity entity);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);

        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        string Max(string tableName, string fieldname, bool hasWithNoLock);
        Query GetAll(string tableName, int? topcount = null);
    }
}
