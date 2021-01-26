using NetWorks.Core.DataAccess.Repositories;
using NetWorks.Core.Domain;
using System.Linq;

namespace NetWorks.Core.Logic
{
    public class DatabaseLogic<TEntity> : IDatabaseLogic<TEntity> where TEntity : BaseEntity, new()
    {
        public IRepo<TEntity> Repo { get; }

        public DatabaseLogic(IRepo<TEntity> repo)
        {
            Repo = repo;
        }

        public int Add(TEntity entity)
        {
            return Repo.Add(entity);
        }

        public int Delete(TEntity entity)
        {
            return Repo.Delete(entity);
        }

        public TEntity Find(int id)
        {
            return Repo.Find(id);
        }

        public IPagedList<TEntity> GetAll(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var entities = Repo.GetAll();
            return new PagedList<TEntity>(entities.ToList(), pageIndex, pageSize);
        }

        public int Update(TEntity entity)
        {
            return Repo.Update(entity);
        }

        public int Delete(int id)
        {
            var entity = Find(id);
            return Delete(entity);
        }
    }
}