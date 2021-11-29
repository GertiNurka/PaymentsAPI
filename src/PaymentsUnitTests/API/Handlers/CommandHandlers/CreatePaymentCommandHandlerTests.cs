using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentsAPI.Application.CQRS.Commands;
using PaymentsAPI.Application.CQRS.Handlers.CommandHandlers;
using PaymentsAPI.Application.Dtos;
using PaymentsAPI.Application.Models;
using PaymentsAPI.Services;
using PaymentsDomain.AggregatesModel.PaymentAggregate;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PaymentsUnitTests.API.Handlers.CommandHandlers
{
    public class CreatePaymentCommandHandlerTests
    {
        private readonly Mock<IPaymentsService> _mockPaymentsService;
        private readonly CreatePaymentCommandHandler _createPaymentCommandHandler;

        public CreatePaymentCommandHandlerTests()
        {
            var mockLogger = new Mock<ILogger<CreatePaymentCommandHandler>>();

            var mockIMapper = new Mock<IMapper>();
            mockIMapper.Setup(s => s.Map<Card>(It.IsAny<CardDto>()))
                .Returns(new Card());
            mockIMapper.Setup(s => s.Map<BillingAddress>(It.IsAny<BillingAddressDto>()))
                .Returns(new BillingAddress());

            _mockPaymentsService = new Mock<IPaymentsService>();
            _mockPaymentsService.Setup(s => s.SavePayment(It.IsAny<Payment>()))
                .ReturnsAsync(new ServiceResponse<bool?>(true, StatusCodes.Status200OK, null, null));
            _mockPaymentsService.Setup(s => s.Save(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ServiceResponse<bool?>(true, StatusCodes.Status200OK, null, null));

            _createPaymentCommandHandler = new CreatePaymentCommandHandler(mockLogger.Object, _mockPaymentsService.Object, mockIMapper.Object);
        }

        [Fact]
        [Trait("Category", "CreatePaymentCommandHandler")]
        public async Task CreatePaymentCommandHandler_ResultNotNull_Test()
        {
            #region Arrange

            var command = new CreatePaymentCommand();

            #endregion

            #region Act

            var response = await _createPaymentCommandHandler.Handle(command, new CancellationToken());

            #endregion

            #region Assert

            Assert.NotNull(response);

            #endregion

        }

        [Fact]
        [Trait("Category", "CreatePaymentCommandHandler")]
        public async Task CreatePaymentCommandHandler_ResultTypeOfObjectResult_Test()
        {
            #region Arrange

            var command = new CreatePaymentCommand();

            #endregion

            #region Act

            var response = await _createPaymentCommandHandler.Handle(command, new CancellationToken());

            #endregion

            #region Assert

            Assert.True(response is ObjectResult);

            #endregion

        }

        [Theory]
        [ClassData(typeof(CommandHandlersTestData.ServiceResponseOk))]
        [Trait("Category", "CreatePaymentCommandHandler")]
        public async Task CreatePaymentCommandHandler_ResultTypeOfCreatedAtRouteResult_Test(bool success, int statusCode)
        {
            #region Arrange

            var command = new CreatePaymentCommand();

            _mockPaymentsService.Setup(s => s.SavePayment(It.IsAny<Payment>()))
                .ReturnsAsync(new ServiceResponse<bool?>(success, statusCode, null, null));
            _mockPaymentsService.Setup(s => s.Save(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ServiceResponse<bool?>(success, statusCode, null, null));

            #endregion

            #region Act

            var response = await _createPaymentCommandHandler.Handle(command, new CancellationToken());

            #endregion

            #region Assert

            Assert.IsType<CreatedAtRouteResult>(response);

            #endregion

        }


        [Theory]
        [ClassData(typeof(CommandHandlersTestData.ServiceResponseFail))]
        [Trait("Category", "CreatePaymentCommandHandler")]
        public async Task CreatePaymentCommandHandler_SavePaymentServiceResponseNotSuccess_ResultTypeOfObjectResult_Test(bool success, int statusCode)
        {
            #region Arrange

            var command = new CreatePaymentCommand();

            _mockPaymentsService.Setup(s => s.SavePayment(It.IsAny<Payment>()))
                .ReturnsAsync(new ServiceResponse<bool?>(success, statusCode, null, null));
            _mockPaymentsService.Setup(s => s.Save(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ServiceResponse<bool?>(success, statusCode, null, null));

            #endregion

            #region Act

            var response = await _createPaymentCommandHandler.Handle(command, new CancellationToken());

            #endregion

            #region Assert

            Assert.True(response is ObjectResult);

            #endregion

        }

        [Theory]
        [ClassData(typeof(CommandHandlersTestData.ServiceResponseFail))]
        [Trait("Category", "CreatePaymentCommandHandler")]
        public async Task CreatePaymentCommandHandler_SaveServiceResponseNotSuccess_ResultTypeOfObjectResult_Test(bool success, int statusCode)
        {
            #region Arrange

            var command = new CreatePaymentCommand();

            _mockPaymentsService.Setup(s => s.Save(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ServiceResponse<bool?>(success, statusCode, null, null));

            #endregion

            #region Act

            var response = await _createPaymentCommandHandler.Handle(command, new CancellationToken());

            #endregion

            #region Assert

            Assert.True(response is ObjectResult);

            #endregion

        }
    }
}
