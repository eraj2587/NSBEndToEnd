using System;
using log4net;
using NServiceBus.Logging;
using NServiceBus.UnitOfWork;
using Storage.Interface;
using ILog = NServiceBus.Logging.ILog;


namespace Host
{
    public class UnitOfWork : IManageUnitsOfWork
    {
        private readonly IRepository _repository;
        private readonly ILog _log;

        public UnitOfWork(IRepository repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }

        public void Begin()
        {
            _log.Info("UoW begin");
        }

        public void End(Exception ex = null)
        {
            _log.Info("UoW ends");
            if (ex != null)
            {
                _log.Error("Error was encountered. Unable to commit changes.", ex);
                return;
            }
            _repository.CommitChanges();

        }
    }
}
