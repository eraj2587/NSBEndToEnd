using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Events
{
   public interface IOrderAccepted 
    {
         int BatchNumber { get;  set; }
         string SourceSystemName { get;  set; }
         DateTime EnteredOn { get;  set; }
         string Comments { get; set; }
    }
}
