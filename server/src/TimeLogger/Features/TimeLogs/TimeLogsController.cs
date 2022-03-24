using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Timelogger.Api.Features.TimeLogs
{
    [ApiController]
    [Route("api/projects")]
    public class TimeLogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimeLogsController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{id}/timelogs")]
        public Task<TimeLogsResponse> Get(int id) => _mediator.Send(new GetTimeLogsQuery(id));

        [HttpDelete("{id}/timelogs/{timeLogId}")]
        public Task Delete(int id, int timeLogId) => _mediator.Send(new DeleteTimeLogCommand(id, timeLogId));

        [HttpPost("{id}/timelogs")]
        public Task<TimeLogResponse> Create(int id, [FromBody] CreateTimeLogCommand request)
        {
            request.ProjectId = id;
            return _mediator.Send(request);
        }
    }
}
