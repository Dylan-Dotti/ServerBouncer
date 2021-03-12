using LoggingLibrary;
using TopshelfBoilerplate;

namespace MockLPService
{
    class Program
    {
        static void Main(string[] args)
        {
            new TopshelfService(
                "SWISSLOG_MOCK_LP_SERVICE",
                "SWISSLOG_MOCK_LP_SERVICE",
                "A mock LP service for testing")
                .Run(new LPServiceWorker(new ConsoleLogger()));
        }
    }
}
