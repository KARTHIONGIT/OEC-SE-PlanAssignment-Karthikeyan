using MediatR;
using Microsoft.EntityFrameworkCore;
using RL.Backend.Exceptions;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;

namespace RL.Backend.Commands.Handlers.Plans
{
    public class AddProcedureUserHandler : IRequestHandler<AddProcedureUserCommand, ApiResponse<Unit>>
    {
        private readonly RLContext _context;

        public AddProcedureUserHandler(RLContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<Unit>> Handle(AddProcedureUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //Validate request
                if (request.PlanId < 1)
                    return ApiResponse<Unit>.Fail(new BadRequestException("Invalid PlanId"));
                if (request.ProcedureId < 1)
                    return ApiResponse<Unit>.Fail(new BadRequestException("Invalid ProcedureId"));
                if (request.UserIds.Any(x => x < 1))
                    return ApiResponse<Unit>.Fail(new BadRequestException("Invalid UserId"));

                if (_context.ProcedureUsers.Any())
                {
                    var entitiesToDeactivate = await _context.ProcedureUsers
                       .Where(pu => pu.PlanId == request.PlanId && pu.ProcedureId == request.ProcedureId && pu.UserId != 0)
                       .ToListAsync();

                    foreach (var entity in entitiesToDeactivate)
                    {
                        entity.UserId = 0;
                    }
                }
                if (request.UserIds.Any())
                {
                    var procUsers = request.UserIds?.Select(id => new ProcedureUser()
                    {
                        PlanId = request.PlanId,
                        ProcedureId = request.ProcedureId,
                        UserId = id,
                    });
                    _context.ProcedureUsers.AddRange(procUsers);
                }
                await _context.SaveChangesAsync();
                return ApiResponse<Unit>.Succeed(new Unit());
            }
            catch (Exception e)
            {
                return ApiResponse<Unit>.Fail(e);
            }
        }
    }
}
