using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RL.Backend.Commands;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;

namespace RL.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly RLContext _context;
    private readonly IMediator _mediator;


    public UsersController(ILogger<UsersController> logger, RLContext context, IMediator mediator)
    {
        _logger = logger;
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [EnableQuery]
    public IEnumerable<User> Get()
    {
        return _context.Users;
    }

    [HttpGet]
    [Route("GetProcedureUsers")]
    public IEnumerable<ProcedureUser> GetProcedureUsers(int planId)
    {
        return _context.ProcedureUsers.Where(x => x.PlanId == planId);
    }

    [HttpPost("AddProcedureUsers")]
    public async Task<IActionResult> AddProcedureUsers([FromBody]AddProcedureUserCommand command, CancellationToken token)
    {
        var response = await _mediator.Send(command, token);

        return response.ToActionResult();
    }

    [HttpDelete("RemoveProcedureUsers")]
    public async Task<IActionResult> DeleteProcedureUsers([FromBody]DeleteProcedureUserCommand command, CancellationToken token)
    {
        var response = await _mediator.Send(command, token);

        return response.ToActionResult();
    }
}
