using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Storage.Interface
{
    public interface IOrderContext
    {
        IDbSet<Order> Orders { get; set; }
        int SaveChanges();
    }
}
