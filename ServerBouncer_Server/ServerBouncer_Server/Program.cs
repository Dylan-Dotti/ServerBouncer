using System;
using System.Net;
using System.Net.Sockets;
using Topshelf;

namespace ServerBouncer_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<SBServer>(s =>
                {
                    s.ConstructUsing(server =>
                    {
                        IPAddress ip = IPAddress.Parse(GetLocalIPAddress());
                        return new SBServer();
                    });
                    s.WhenStarted(server => server.Start());
                    s.WhenStopped(server => server.Stop());
                });
                x.SetServiceName("SWISSLOG_SERVER_BOUNCER");
                x.SetDisplayName("SWISSLOG_SERVER_BOUNCER");
                x.SetDescription("App for automating Swisslog service restarts");
            });
            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
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
