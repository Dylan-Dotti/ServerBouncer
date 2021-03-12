using LoggingLibrary;
using System;
using TopshelfBoilerplate;

namespace MockLPService
{
    class LPServiceWorker : IServiceWorker
    {
        private readonly ILogger logger;

        public LPServiceWorker(ILogger logger)
        {
            this.logger = logger;
        }

        public void Start()
        {
            logger.Log("LP service started");
        }

        public void Stop()
        {
            logger.Log("LP service stopped");
        }
    }
}
