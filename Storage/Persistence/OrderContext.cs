using System.Data.Common;
using System.Data.Entity;
using Domain;
using Storage.Interface;

namespace Storage.Persistence
{
    public class OrderContext : DbContext, IOrderContext
    {
        public OrderContext()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        public OrderContext(string connectionStringName)
            : base(connectionStringName)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

        }
        public OrderContext(DbConnection dbConnection)
            : base(dbConnection, false)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }


        public IDbSet<Order> Orders { get; set; }
    }
}
