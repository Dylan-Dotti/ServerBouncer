﻿using LoggingLibrary;
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

        protected async override void OnServerStarted()
        {
            logger.Log($"Server running on port {port}...");
            //LogSettings();
            await new ServiceBounceSequence(
                AppSettings.ServiceNameStopSequence,
                AppSettings.ServiceNameStartSequence,
                AppSettings.PostStopDelaysInMilliseconds,
                AppSettings.PostStartDelaysInMilliseconds,
                logger)
                .ExecuteAsync();
        }

        protected override void OnServerStopped()
        {
            logger.Log("Server stopped");
        }

        private void LogSettings()
        {
            logger.Log("Bouncer service name:");
            logger.Log(AppSettings.BouncerServiceName);
            logger.Log("Stop sequence:");
            foreach (string serviceName in AppSettings.ServiceNameStopSequence)
            {
                logger.Log(serviceName);
            }
            logger.Log("Stop delays:");
            foreach (float delay in AppSettings.PostStopDelaysInMilliseconds)
            {
                logger.Log(delay.ToString());
            }
            logger.Log("Start sequence:");
            foreach (string serviceName in AppSettings.ServiceNameStartSequence)
            {
                logger.Log(serviceName);
            }
            logger.Log("Start delays:");
            foreach (float delay in AppSettings.PostStartDelaysInMilliseconds)
            {
                logger.Log(delay.ToString());
            }
        }
    }
}
