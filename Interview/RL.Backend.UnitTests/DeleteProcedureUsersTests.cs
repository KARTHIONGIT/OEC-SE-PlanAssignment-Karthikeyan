using FluentAssertions;
using Moq;
using RL.Backend.Commands;
using RL.Backend.Commands.Handlers.Plans;
using RL.Backend.Exceptions;
using RL.Data;

namespace RL.Backend.UnitTests
{
    [TestClass]
    internal class DeleteProcedureUsersTests
    {
        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(int.MinValue)]
        public async Task DeleteProcedureToPlanTests_InvalidPlanId_ReturnsBadRequest(int planId)
        {
            //Given
            var context = new Mock<RLContext>();
            var sut = new DeleteProcedureUserHandler(context.Object);
            var request = new DeleteProcedureUserCommand()
            {
                PlanId = planId,
                ProcedureId = 1,
            };
            //When
            var result = await sut.Handle(request, new CancellationToken());

            //Then
            result.Exception.Should().BeOfType(typeof(BadRequestException));
            result.Succeeded.Should().BeFalse();
        }
        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(int.MinValue)]
        public async Task DeleteProcedureToPlanTests_InvalidProcedureId_ReturnsBadRequest(int procedureId)
        {
            //Given
            var context = new Mock<RLContext>();
            var sut = new DeleteProcedureUserHandler(context.Object);
            var request = new DeleteProcedureUserCommand()
            {
                PlanId = 1,
                ProcedureId = procedureId,
            };
            //When
            var result = await sut.Handle(request, new CancellationToken());

            //Then
            result.Exception.Should().BeOfType(typeof(BadRequestException));
            result.Succeeded.Should().BeFalse();
        }
        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(int.MinValue)]
        public async Task DeleteProcedureToPlanTests_InvalidUserId_ReturnsBadRequest(int userId)
        {
            //Given
            var context = new Mock<RLContext>();
            var sut = new DeleteProcedureUserHandler(context.Object);
            var request = new DeleteProcedureUserCommand()
            {
                PlanId = 2,
                ProcedureId = 1,
            };
            //When
            var result = await sut.Handle(request, new CancellationToken());

            //Then
            result.Exception.Should().BeOfType(typeof(BadRequestException));
            result.Succeeded.Should().BeFalse();
        }
    }
}
