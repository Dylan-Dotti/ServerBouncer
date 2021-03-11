using System;
using System.Net;
using System.Net.Sockets;
using TcpServerLibrary;
using TopshelfBoilerplate;

namespace ServerBouncer_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            string serviceName = "SWISSLOG_SERVER_BOUNCER";
            string displayName = "SWISSLOG_SERVER_BOUNCER";
            string description = "App for automating Swisslog service restarts";
            ILogger logger = new FileLogger("C:/Users/h4dottd/Desktop/SB_Logs/ServerBouncer.log.txt");
            IService service = new SBServer(GetLocalIPAddress(), 58, logger);
            new TopshelfService(serviceName, displayName, description).Run(service);
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
