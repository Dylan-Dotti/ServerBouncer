using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TcpServerLibrary;

namespace ServerBouncer_Server
{
    class ClientHandler
    {
        public void HandleClient(BinaryTcpClient client, ILogger logger)
        {

        }

        public void HandleClientThreaded(BinaryTcpClient client, ILogger logger)
        {
            ThreadPool.QueueUserWorkItem(s => HandleClient(client, logger));
        }
    }
}
