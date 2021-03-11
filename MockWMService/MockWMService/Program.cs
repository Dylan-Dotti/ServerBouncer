using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            IService service = new WMService();
            new TopshelfService(serviceName, displayName, description).Run(service);
        }
    }
}
