using Timelogger.Domain;

namespace Timelogger.Api.Features.TimeLogs
{
    public class TimeLogsResponse
    {
        public IEnumerable<TimeLog> TimeRegistrations { get; set; } = Enumerable.Empty<TimeLog>();
    }
}
