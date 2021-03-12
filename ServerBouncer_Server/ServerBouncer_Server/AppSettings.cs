using System.Collections.Generic;
using System.Linq;

namespace ServerBouncer_Server
{
    static class AppSettings
    {
        public static string BouncerServiceName { get; private set; }

        public static IReadOnlyList<string> ServiceNameStopSequence { get; private set; }
        public static IReadOnlyList<string> ServiceNameStartSequence { get; private set; }

        public static IReadOnlyList<int> PostStopDelaysInMilliseconds { get; private set; }
        public static IReadOnlyList<int> PostStartDelaysInMilliseconds { get; private set; }

        static AppSettings()
        {
            var settings = Properties.Settings.Default;
            BouncerServiceName = settings.BouncerServiceName;
            ServiceNameStopSequence = ProcessStringArray(settings.StopSequence);
            ServiceNameStartSequence = ProcessStringArray(settings.StartSequence);

            PostStopDelaysInMilliseconds = ProcessStringArray(settings.PostStopDelayMilliseconds)
                .Select(s => int.Parse(s)).ToList();
            PostStartDelaysInMilliseconds = ProcessStringArray(settings.PostStartDelayMilliseconds)
                .Select(s => int.Parse(s)).ToList();
        }

        private static List<string> ProcessStringArray(string input)
        {
            return input.Split(',').Select(s => s.Trim()).ToList();
        }
    }
}
