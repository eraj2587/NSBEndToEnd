using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Storage.Infrastructure;

namespace Storage.Interface
{
    public interface IRepository
    {
        TAggregate Add<TAggregate>(TAggregate aggregate) where TAggregate : class;
        TAggregate Load<TAggregate>(Expression<Func<TAggregate, bool>> predicate, params Expression<Func<TAggregate, object>>[] includes) where TAggregate : class;
        QueryResult<TResult> Search<TResult>(IQuery<TResult> query);
        TProjection Project<TAggregate, TProjection>(Func<IQueryable<TAggregate>, TProjection> query) where TAggregate : class;
        void CommitChanges();
    }
}
