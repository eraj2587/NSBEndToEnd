using System;
using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Storage.Interface;

namespace ServiceHost.Handlers
{
    public class SubmitOrderHandler : IHandleMessages<SubmitOrder>
    {
        readonly IRepository _repository;
        readonly ILog _log;
        readonly IBus _bus;

        public SubmitOrderHandler(IRepository repository, ILog log, IBus bus)
        {
            _bus = bus;
            _repository = repository;
            _log = log;
        }

        public void Handle(SubmitOrder message)
        {
            _bus.Publish<IOrderAccepted>(e =>
            {
                e.BatchNumber=message.BatchNumber;
                e.Comments="Order Accepted";
                e.EnteredOn = message.EnteredOn;
                e.SourceSystemName = message.SourceSystemName;
            });

            Console.WriteLine("Order with batch # : " + message.BatchNumber.ToString() + " submitted send for acceptance");

        }
    }
}
