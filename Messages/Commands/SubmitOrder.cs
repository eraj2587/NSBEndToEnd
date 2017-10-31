using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Commands
{
    public class SubmitOrder:BaseCommand
    {
        public int BatchNumber { get;  set; }
        public string SourceSystemName { get;  set; }
        public DateTime EnteredOn { get;  set; }
    }

    public abstract class BaseCommand
    {
        public Guid Id { get; set; }

        internal BaseCommand()
        {
            if (Id == Guid.Empty) Id = Guid.NewGuid();
        }
    }
}
