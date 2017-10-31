using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Host
{
    class Constants
    {
        public const string NServiceBus_DataBusBasePath = "NServiceBus.DataBus.BasePath";
    }

    public static class NSBExtentions
    {
        public static ConventionsBuilder OrderConventions(this ConventionsBuilder builder)
        {
            return builder
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.Contains("Messages.Commands"))
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains("Messages.Events"))
                .DefiningEncryptedPropertiesAs(p => p.Name.EndsWith("Encrypted"))
                .DefiningDataBusPropertiesAs(p => p.Name.EndsWith("DataBus"))
                .DefiningExpressMessagesAs(t => t.Name.EndsWith("Express"));
        }
    }

}
