using MediatR;
using Microsoft.EntityFrameworkCore;
using RL.Backend.Exceptions;
using RL.Backend.Models;
using RL.Data;

namespace RL.Backend.Commands.Handlers.Plans
{
    public class DeleteProcedureUserHandler : IRequestHandler<DeleteProcedureUserCommand, ApiResponse<Unit>>
    {
        private readonly RLContext _context;

        public DeleteProcedureUserHandler(RLContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<Unit>> Handle(DeleteProcedureUserCommand request, CancellationToken cancellationToken)
        {
            try
            {              
                if (_context.ProcedureUsers == null)
                {
                    return ApiResponse<Unit>.Fail(new Exception("Add users first to delete from database"));
                }

                var entitiesToDelete = await _context.ProcedureUsers
               .Where(pu => pu.UserId == 0)
               .ToListAsync();

                if (entitiesToDelete == null || !entitiesToDelete.Any())
                {
                    return ApiResponse<Unit>.Fail(new Exception("Not records available for delete"));
                }
                _context.ProcedureUsers.RemoveRange(entitiesToDelete);
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
