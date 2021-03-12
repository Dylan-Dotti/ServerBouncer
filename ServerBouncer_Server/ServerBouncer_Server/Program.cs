using LoggingLibrary;
using System;
using System.Net;
using System.Net.Sockets;
using TopshelfBoilerplate;

namespace ServerBouncer_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            string serviceName = "SWISSLOG_SERVER_BOUNCER";
            string displayName = "SWISSLOG_SERVER_BOUNCER";
            string description = "App for automating service restarts";
            ILogger logger = new ConsoleLogger();
                //new FileLogger("C:/Users/h4dottd/Desktop/SB_Logs/ServerBouncer.log.txt");
            IServiceWorker worker = new SBServer(GetLocalIPAddress(), 58, logger);
            new TopshelfService(serviceName, displayName, description).Run(worker);
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
