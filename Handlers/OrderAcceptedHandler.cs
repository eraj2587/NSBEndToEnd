using System;
using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Storage.Interface;

namespace ServiceHost.Handlers
{
    public class OrderAcceptedHandler : IHandleMessages<IOrderAccepted>
    {
        readonly IRepository _repository;
        readonly ILog _log;

        public  OrderAcceptedHandler(IRepository repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }

        public void Handle(IOrderAccepted message)
        {
            _repository.Add<Domain.Order>(new Domain.Order(message.BatchNumber,message.SourceSystemName,message.EnteredOn,message.Comments));
            Console.WriteLine("Order with batch # : " + message.BatchNumber.ToString() + " added and ready for dispatch.");
        }
    }
}
