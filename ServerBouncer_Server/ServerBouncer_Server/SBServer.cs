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
        private readonly ClientHandler clientHandler;

        public SBServer(string ip, int port, ILogger logger) : base(ip, port)
        {
            this.logger = logger;
            clientHandler = new ClientHandler();
        }

        protected override void OnAcceptClient(BinaryTcpClient client)
        {
            logger.Log("Client connected");
            clientHandler.HandleClientThreaded(client, logger);
        }

        protected override void OnServerStarted()
        {
            logger.Log($"Server running on port {port}...");
            try
            {
                var serviceInterface = new ServiceInterface("SWISSLOG_MOCK_WM_SERVICE");
                logger.Log("Stopping service...");
                serviceInterface.StopService();
                logger.Log("Service stopped");
                Thread.Sleep(20000);
                logger.Log("Starting service...");
                serviceInterface.StartService();
                logger.Log("Service started");
            }
            catch (Exception e)
            {
                logger.Log(e.ToString());
            }
        }

        protected override void OnServerStopped()
        {
            logger.Log("Server stopped");
        }
    }
}
