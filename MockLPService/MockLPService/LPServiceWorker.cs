using System;
using TopshelfBoilerplate;

namespace MockLPService
{
    class LPServiceWorker : IServiceWorker
    {
        public void Start()
        {
            Console.WriteLine("LP service started");
        }

        public void Stop()
        {
            Console.WriteLine("LP service stopped");
        }
    }
}
