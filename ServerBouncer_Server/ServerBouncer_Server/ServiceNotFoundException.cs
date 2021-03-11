using System;

namespace ServerBouncer_Server
{
    class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException(string serviceName)
            : base($"The service with service name {serviceName} was not found.")
        { }
    }
}
