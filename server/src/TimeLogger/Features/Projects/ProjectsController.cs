using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Timelogger.Api.Features.Projects
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public Task<ProjectsResponse> Get() => _mediator.Send(new GetProjectsQuery());

        [HttpGet("{id}")]
        public Task<ProjectResponse> GetById(int id) => _mediator.Send(new GetProjectQuery(id));

        [HttpPut("{id}/complete")]
        public Task Complete(int id) => _mediator.Send(new CompleteProjectCommand(id));
    }
}
