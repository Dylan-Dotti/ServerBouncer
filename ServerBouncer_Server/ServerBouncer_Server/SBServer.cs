using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using TcpServerLibrary;
using TopshelfBoilerplate;

namespace ServerBouncer_Server
{
    class SBServer : TcpServer, IService
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
            var serviceNames = ServiceController.GetServices().Select(s => s.ServiceName);
            foreach (string name in serviceNames)
            {
                logger.Log(name);
            }
        }

        protected override void OnServerStopped()
        {
            logger.Log("Server stopped");
        }
    }
}
