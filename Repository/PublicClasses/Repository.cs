using Repository.PublicInterfaces;
using System.Linq.Expressions;
using Data;
using PetaPoco;
using Data.Interfaces;
using Utilities;
using Data.Base;
using PetaPoco.Core;
using Dto.Common;

namespace Repository.PublicClasses
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class

    {
        private readonly object _lock = new();

        private readonly IDBContext dbContext;

        private static List<TEntity> _entitiesCache = new List<TEntity>();

        private static DateTime _cacheTime = DateTime.Now;

        public Repository(IDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<TEntity> EntitiesCache
        {
            get
            {
                if (_entitiesCache == null || _entitiesCache.Count == 0 || _cacheTime < DateTime.Now.AddDays(1))
                {
                    lock (_lock)
                    {
                        if (_entitiesCache == null || _entitiesCache.Count == 0 || _cacheTime < DateTime.Now.AddDays(1))
                        {
                            _entitiesCache = dbContext.Database.FetchAsync<TEntity>().Result;
                            _cacheTime = DateTime.Now.AddDays(1);
                        }
                    }
                }

                return _entitiesCache;
            }
        }

        public IDBContext DBContext => dbContext;

        #region Async Methods

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Assert.NotNull(entity, nameof(TEntity));
            await dbContext.Database.InsertAsync(cancellationToken, entity);

        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Assert.NotNull(entity, nameof(entity));
            await dbContext.Database.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Assert.NotNull(entity, nameof(entity));
            await dbContext.Database.DeleteAsync(entity);
        }

        #endregion

        #region Sync Methods

        public virtual void Add(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            dbContext.Database.Insert(entity);
        }

        public virtual void Update(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            dbContext.Database.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            dbContext.Database.Delete(entity);
        }

        public virtual string Max(string tableName, string fieldname, bool hasWithNoLock)
        {
            var sql = new Sql();
            sql.Select($"Max({fieldname})");
            sql.From($"{tableName}");
            if (hasWithNoLock)
                sql.Append("with(nolock)");
            return sql.SQL;
        }

        public virtual Query GetAll(string tableName, int? topcount = null)
        {
            return new Query(tableName, true, topcount);
        }

        #endregion
    }
}
