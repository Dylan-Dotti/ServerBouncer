using System;
using System.Linq;
using System.ServiceProcess;

namespace ServerBouncer_Server
{
    class ServiceInterface
    {
        private readonly ServiceController controller;

        public string ServiceName => controller.ServiceName;

        public ServiceInterface(string serviceName)
        {
            try
            {
                controller = ServiceController.GetServices()
                    .Single(s => s.ServiceName == serviceName);
            }
            catch
            {
                throw new ServiceNotFoundException(serviceName);
            }
        }

        public void StartService(double timeoutMilliseconds = 60000)
        {
            TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
            controller.Start();
            controller.WaitForStatus(ServiceControllerStatus.Running, timeout);
        }

        public void StopService(double timeoutMilliseconds = 60000)
        {
            TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
            controller.Stop();
            controller.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
        }
    }
}
