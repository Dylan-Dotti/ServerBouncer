using TopshelfBoilerplate;

namespace MockWMService
{
    class Program
    {
        static void Main(string[] args)
        {
            string serviceName = "SWISSLOG_MOCK_WM_SERVICE";
            string displayName = "SWISSLOG_MOCK_WM_SERVICE";
            string description = "A mock WM service for testing";
            IServiceWorker service = new WMService();
            new TopshelfService(serviceName, displayName, description).Run(service);
        }
    }
}
