using MediatR;
using RL.Backend.Models;

namespace RL.Backend.Commands
{
    public class DeleteProcedureUserCommand : IRequest<ApiResponse<Unit>>
    {
    }
}
