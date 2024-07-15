using MediatR;
using RL.Backend.Models;
using RL.Data.DataModels;

namespace RL.Backend.Commands
{
    public class AddProcedureUserCommand : IRequest<ApiResponse<Unit>>
    {
        public AddProcedureUserCommand(int planId, int procedureId)
        {
            PlanId = planId;
            ProcedureId = procedureId;
        }

        public  int PlanId { get; set; }
        public int ProcedureId { get; set; }
        public IEnumerable<int>? UserIds { get; set; }
    }
}
