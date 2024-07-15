using Moq;
using RL.Backend.Commands.Handlers.Plans;
using RL.Backend.Commands;
using RL.Backend.Exceptions;
using RL.Data;
using FluentAssertions;

namespace RL.Backend.UnitTests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(int.MinValue)]
        public async Task UserControllerTest_AddProcedureUsers(int planId)
        {
            //Given
            //TODo - yet to commit
        }
    }
}
