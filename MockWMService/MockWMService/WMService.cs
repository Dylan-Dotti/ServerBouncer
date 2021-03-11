using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopshelfBoilerplate;

namespace MockWMService
{
    class WMService : IService
    {
        public void Start()
        {
            Console.WriteLine("WMService is running...");
        }

        public void Stop()
        {
            Console.WriteLine("WMService has stopped");
        }
    }
}
