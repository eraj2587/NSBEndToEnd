using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using Storage.Interface;
using Storage.Persistence;


namespace Storage.Infrastructure
{
    public sealed class Repository : IRepository, IDisposable
    {
        readonly Lazy<IOrderContext> _context;

        public Repository(IOrderContext context)
        {
            _context = new Lazy<IOrderContext>(() => context);
        }
        public Repository(Func<IOrderContext> getContext)
        {
            _context = new Lazy<IOrderContext>(getContext);
        }

        /// <summary>
        /// Add a new entity to database
        /// </summary>
        public TAggregate Add<TAggregate>(TAggregate aggregate) where TAggregate : class
        {
            return ((OrderContext)_context.Value).Set<TAggregate>().Add(aggregate);
        }

        /// <summary>
        /// Load an entity by Id with optional includes
        /// </summary>
        public TAggregate Load<TAggregate>(Expression<Func<TAggregate, bool>> predicate, params Expression<Func<TAggregate, object>>[] includes)
            where TAggregate : class
        {
            var q = ((OrderContext)_context.Value).Set<TAggregate>().AsQueryable();

            if (includes != null)
            {
                q = includes.Aggregate(q, (set, inc) => set.Include(inc));
            }

            return q.SingleOrDefault(predicate);
        }

        public QueryResult<TResult> Search<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var handler = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .SingleOrDefault(x => handlerType.IsAssignableFrom(x));

            if (handler == null) return null;
            dynamic q = Activator.CreateInstance(handler, _context);
            return q.Handle((dynamic)q);
        }

        public TProjection Project<TAggregate, TProjection>(Func<IQueryable<TAggregate>, TProjection> query) where TAggregate : class
        {
            return query(((OrderContext)_context.Value).Set<TAggregate>().AsQueryable());
        }

        void Commit()
        {
            if (_context.IsValueCreated)
                _context.Value.SaveChanges();
        }
        public void CommitChanges()
        {
            Commit();
        }

        public void Dispose()
        {

        }
    }
}
