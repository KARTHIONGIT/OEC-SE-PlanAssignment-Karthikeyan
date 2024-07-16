using RL.Backend.Commands;
using RL.Data.DataModels;
using System.Numerics;

namespace RL.Backend.UnitTests.Mock
{
    public static class TestObject
    {
        public static AddProcedureUserCommand GetAddProcedureUserCommandObject()
        {
            return new AddProcedureUserCommand()
            {
                PlanId= 1,
                ProcedureId= 2,
                UserIds = new[] { 1, 2, 3 }
            };
        }
        public static IEnumerable<ProcedureUser> GetProcedureUserObject()
        {
            return new ProcedureUser[] {
                new ProcedureUser()
                {
                    PlanId = 1,
                    ProcedureId = 2,
                    UserId = 1
                },
                new ProcedureUser()
                {
                    PlanId = 1,
                    ProcedureId = 2,
                    UserId = 1
                },
                new ProcedureUser()
                {
                    PlanId = 1,
                    ProcedureId = 2,
                    UserId = 1
                }
            };
        }
    }
}
