using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RL.Backend.Commands;
using RL.Backend.Controllers;
using RL.Backend.Exceptions;
using RL.Backend.Models;
using RL.Backend.UnitTests.Mock;
using RL.Data;

namespace RL.Backend.UnitTests
{
    [TestClass]
    public class UserControllerTests
    {
        public Mock<IMediator> mediatorMock;
        public Mock<ILogger<UsersController>> loggerMock;
        public Mock<RLContext> contextMock;
        public UsersController controllerTestInstance;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mediatorMock = new Mock<IMediator>();
            this.loggerMock = new Mock<ILogger<UsersController>>();
            this.contextMock = new Mock<RLContext>();
            controllerTestInstance = new UsersController(loggerMock.Object, contextMock.Object, mediatorMock.Object);
        }

        [TestMethod]
        public void ThrowExpceptionIfMedidatorObjectIsNotInjected()
        {
            var result = Assert.ThrowsException<ArgumentNullException>(() => new UsersController(loggerMock.Object, contextMock.Object,null));
            Assert.AreEqual(result.Message, "Value cannot be null. (Parameter 'mediator')");
        }

        [TestMethod]
        public async Task UserControllerTest_AddUser_HappyPath()
        {
            AddProcedureUserCommand responseModels = new();
            var procedureUserObject = TestObject.GetAddProcedureUserCommandObject();

            mediatorMock.Setup(m => m.Send(It.IsAny<AddProcedureUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ApiResponse<Unit>.Succeed(new Unit()));

            var response = await controllerTestInstance.AddProcedureUsers(procedureUserObject, It.IsAny<CancellationToken>());

            this.mediatorMock.Verify(x => x.Send(It.IsAny<AddProcedureUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            var result = (OkResult)response;
            Assert.AreEqual(200, result?.StatusCode);
        }
        
        [TestMethod]
        public async Task UserControllerTest_DeleteUser_HappyPath()
        {
            DeleteProcedureUserCommand responseModels = new();

            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteProcedureUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ApiResponse<Unit>.Succeed(new Unit()));

            var response = await controllerTestInstance.DeleteProcedureUsers(responseModels, It.IsAny<CancellationToken>());

            this.mediatorMock.Verify(x => x.Send(It.IsAny<DeleteProcedureUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            var result = (OkResult)response;
            Assert.AreEqual(200, result?.StatusCode);
        }
        
        [TestMethod]
        public async Task UserControllerTest_GetUser_ThrowsBadRequestException()
        {
            Assert.ThrowsException<BadRequestException>(() =>
           controllerTestInstance.GetProcedureUsers(0));
        }
        [TestMethod]
        public async Task UserControllerTest_DeleteUser_ThrowsBadRequestException()
        {
            DeleteProcedureUserCommand responseModels = new();

            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteProcedureUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ApiResponse<Unit>.Fail(new BadRequestException("Add users first to delete from database")));

            var response = await controllerTestInstance.DeleteProcedureUsers(responseModels, It.IsAny<CancellationToken>());

            this.mediatorMock.Verify(x => x.Send(It.IsAny<DeleteProcedureUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            var result = (BadRequestObjectResult)response;
            Assert.AreEqual(400, result?.StatusCode);
        }
    }
}
