using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentsAPI.Application.CQRS.Handlers.QueryHandlers;
using PaymentsAPI.Application.CQRS.Queries;
using PaymentsAPI.Application.Dtos;
using PaymentsAPI.Application.Models;
using PaymentsAPI.Services;
using PaymentsDomain.AggregatesModel.PaymentAggregate;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PaymentsUnitTests.API.Handlers.QueryHandlers
{
    public class GetPaymentQueryHandlerTests
    {
        private readonly Mock<IPaymentsService> _mockIPaymentsService;
        private readonly GetPaymentQueryHandler _getPaymentQueryHandler;

        public GetPaymentQueryHandlerTests()
        {
            var mockIMapper = new Mock<IMapper>();
            mockIMapper.Setup(s => s.Map<PaymentDto>(It.IsAny<Payment>()))
                .Returns(new PaymentDto());

            var mockLogger = new Mock<ILogger<GetPaymentQueryHandler>>();

            _mockIPaymentsService = new Mock<IPaymentsService>();

            _getPaymentQueryHandler = new GetPaymentQueryHandler(mockLogger.Object, _mockIPaymentsService.Object, mockIMapper.Object);
        }

        [Fact]
        [Trait("Category", "GetPaymentQueryHandler")]
        public async Task GetPaymentQueryHandler_ResultNotNull_Test()
        {
            #region Arrange

            var payment = new Payment();

            _mockIPaymentsService.Setup(s => s.Get(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<Payment>(true, StatusCodes.Status200OK, null, payment));

            #endregion

            #region Act

            var response = await _getPaymentQueryHandler.Handle(new GetPaymentQuery(1), new CancellationToken());

            #endregion

            #region Assert

            Assert.NotNull(response);

            #endregion

        }

        [Fact]
        [Trait("Category", "GetPaymentQueryHandler")]
        public async Task GetPaymentQueryHandler_ResultTypeOfObjectResult_Test()
        {
            #region Arrange

            var payment = new Payment();

            _mockIPaymentsService.Setup(s => s.Get(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<Payment>(true, StatusCodes.Status200OK, null, payment));

            #endregion

            #region Act

            var response = await _getPaymentQueryHandler.Handle(new GetPaymentQuery(1), new CancellationToken());

            #endregion

            #region Assert

            Assert.True(response is ObjectResult);

            #endregion

        }

        [Fact]
        [Trait("Category", "GetPaymentQueryHandler")]
        public async Task GetPaymentQueryHandler_DataTypeOfPaymentDto_Test()
        {
            #region Arrange

            var payment = new Payment();

            _mockIPaymentsService.Setup(s => s.Get(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<Payment>(true, StatusCodes.Status200OK, null, payment));

            #endregion

            #region Act

            var response = await _getPaymentQueryHandler.Handle(new GetPaymentQuery(1), new CancellationToken());

            #endregion

            #region Assert

            Assert.IsType<PaymentDto>(response.Value);

            #endregion

        }

        [Theory]
        [ClassData(typeof(QueryHandlersTestData.ServiceResponseFail))]
        [Trait("Category", "GetPaymentQueryHandler")]
        public async Task GetPaymentQueryHandler_ServiceResponseFailed_ResultTypeOfObjectResult_Test(bool success, int statusCode)
        {
            #region Arrange

            _mockIPaymentsService.Setup(s => s.Get(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<Payment>(success, statusCode, null, null));

            #endregion

            #region Act

            var response = await _getPaymentQueryHandler.Handle(new GetPaymentQuery(1), new CancellationToken());

            #endregion

            #region Assert

            Assert.True(response is ObjectResult);

            #endregion
        }
    }
}
