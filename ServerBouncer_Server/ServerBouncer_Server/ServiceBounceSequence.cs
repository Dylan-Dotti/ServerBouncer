using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerBouncer_Server
{
    class ServiceBounceSequence
    {
        private readonly IReadOnlyList<string> stopSequence;
        private readonly IReadOnlyList<string> startSequence;
        private readonly IReadOnlyList<int> stopDelays;
        private readonly IReadOnlyList<int> startDelays;

        private readonly ILogger logger;

        public ServiceBounceSequence(
            IEnumerable<string> serviceStopSequence,
            IEnumerable<string> serviceStartSequence,
            IEnumerable<int> postStopDelaysInMilliseconds,
            IEnumerable<int> postStartDelaysInMilliseconds,
            ILogger logger)
        {
            stopSequence = serviceStopSequence.ToList();
            startSequence = serviceStartSequence.ToList();
            stopDelays = postStopDelaysInMilliseconds.ToList();
            startDelays = postStartDelaysInMilliseconds.ToList();
            this.logger = logger;
        }

        public async Task ExecuteAsync()
        {
            // assumes stop sequence and start sequence have the same services
            // assumes stopDelays is the same length as stopSequence
            try
            {
                var serviceInterfaces = stopSequence
                    .Select(s => new ServiceInterface(s));
                // stop services
                for (int i = 0; i < stopSequence.Count; i++)
                {
                    ServiceInterface service = serviceInterfaces
                        .Single(s => s.ServiceName == stopSequence[i]);
                    logger.Log($"Stopping service: {stopSequence[i]}...");
                    service.StopService();
                    logger.Log("Service stopped");
                    logger.Log($"Delaying for {stopDelays[i]} milliseconds...");
                    await Task.Delay(stopDelays[i]);
                    logger.Log("Delay complete");
                }
                // start services
                for (int i = 0; i < startSequence.Count; i++)
                {
                    ServiceInterface service = serviceInterfaces
                        .Single(s => s.ServiceName == startSequence[i]);
                    logger.Log($"Starting service: {startSequence[i]}...");
                    service.StartService();
                    logger.Log("Service started");
                    if (i < startDelays.Count)
                    {
                        logger.Log($"Delaying for {startDelays[i]} milliseconds...");
                        await Task.Delay(startDelays[i]);
                        logger.Log("Delay complete");
                    }
                }
            }
            catch (Exception e)
            {
                logger.Log(e.ToString());
            }
        }
    }
}
