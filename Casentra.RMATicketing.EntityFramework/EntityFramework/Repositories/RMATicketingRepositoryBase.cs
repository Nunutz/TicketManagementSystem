using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Casentra.RMATicketing.EntityFramework.Repositories
{
    public abstract class RMATicketingRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<RMATicketingDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected RMATicketingRepositoryBase(IDbContextProvider<RMATicketingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class RMATicketingRepositoryBase<TEntity> : RMATicketingRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected RMATicketingRepositoryBase(IDbContextProvider<RMATicketingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
