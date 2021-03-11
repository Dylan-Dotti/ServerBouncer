using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TcpServerLibrary;
using TopshelfBoilerplate;

namespace ServerBouncer_Server
{
    class SBServer : TcpServer, IServiceWorker
    {
        private readonly ILogger logger;

        public SBServer(string ip, int port, ILogger logger) : base(ip, port)
        {
            this.logger = logger;
        }

        protected override void OnAcceptClient(BinaryTcpClient client)
        {
            logger.Log("Client connected");
            ThreadPool.QueueUserWorkItem(state =>
            {
                while (IsRunning)
                {
                    Thread.Sleep(1000);
                    logger.Log("Server is listening...");
                }
            });
        }

        protected override void OnServerStarted()
        {
            logger.Log($"Server running on port {port}...");
        }

        protected override void OnServerStopped()
        {
            logger.Log("Server stopped");
        }
    }
}
