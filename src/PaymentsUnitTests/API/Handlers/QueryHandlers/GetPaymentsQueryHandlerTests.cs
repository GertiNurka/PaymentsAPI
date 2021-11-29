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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PaymentsUnitTests.API.Handlers.QueryHandlers
{
    public class GetPaymentsQueryHandlerTests
    {
        private readonly Mock<IPaymentsService> _mockIPaymentsService;
        private readonly GetPaymentsQueryHandler _getPaymentsQueryHandler;

        public GetPaymentsQueryHandlerTests()
        {
            var mockIMapper = new Mock<IMapper>();
            mockIMapper.Setup(s => s.Map<IEnumerable<PaymentDto>>(It.IsAny<IEnumerable<Payment>>()))
                .Returns(new List<PaymentDto>());

            var mockLogger = new Mock<ILogger<GetPaymentsQueryHandler>>();
            _mockIPaymentsService = new Mock<IPaymentsService>();

            _getPaymentsQueryHandler = new GetPaymentsQueryHandler(mockLogger.Object, _mockIPaymentsService.Object, mockIMapper.Object);
        }

        [Fact]
        [Trait("Category", "GetPaymentsQueryHandler")]
        public async Task GetPaymentsQueryHandler_ResultNotNull_Test()
        {
            #region Arrange

            var payments = new List<Payment>();

            _mockIPaymentsService.Setup(s => s.Get())
                .ReturnsAsync(new ServiceResponse<IEnumerable<Payment>>(true, StatusCodes.Status200OK, null, payments));
                

            #endregion

            #region Act

            var response = await _getPaymentsQueryHandler.Handle(new GetPaymentsQuery(), new CancellationToken());

            #endregion

            #region Assert

            Assert.NotNull(response);

            #endregion

        }

        [Fact]
        [Trait("Category", "GetPaymentsQueryHandler")]
        public async Task GetPaymentsQueryHandler_ResultTypeOfObjectResult_Test()
        {
            #region Arrange

            var payments = new List<Payment>();

            _mockIPaymentsService.Setup(s => s.Get())
                .ReturnsAsync(new ServiceResponse<IEnumerable<Payment>>(true, StatusCodes.Status200OK, null, payments));


            #endregion

            #region Act

            var response = await _getPaymentsQueryHandler.Handle(new GetPaymentsQuery(), new CancellationToken());

            #endregion

            #region Assert

            Assert.True(response is ObjectResult);

            #endregion

        }

        [Fact]
        [Trait("Category", "GetPaymentsQueryHandler")]
        public async Task GetPaymentsQueryHandler_DataTypeOfListPaymentDto_Test()
        {
            #region Arrange

            var payments = new List<Payment>();

            _mockIPaymentsService.Setup(s => s.Get())
                .ReturnsAsync(new ServiceResponse<IEnumerable<Payment>>(true, StatusCodes.Status200OK, null, payments));

            #endregion

            #region Act

            var response = await _getPaymentsQueryHandler.Handle(new GetPaymentsQuery(), new CancellationToken());

            #endregion

            #region Assert

            Assert.IsAssignableFrom<IEnumerable<PaymentDto>>(response.Value);

            #endregion

        }

        [Theory]
        [ClassData(typeof(QueryHandlersTestData.ServiceResponseFail))]
        [Trait("Category", "GetPaymentsQueryHandler")]
        public async Task GetPaymentsQueryHandler_ServiceResponseFailed_ResultTypeOfObjectResult_Test(bool success, int statusCode)
        {
            #region Arrange

            _mockIPaymentsService.Setup(s => s.Get())
                .ReturnsAsync(new ServiceResponse<IEnumerable<Payment>>(success, statusCode, null, null));

            #endregion

            #region Act

            var response = await _getPaymentsQueryHandler.Handle(new GetPaymentsQuery(), new CancellationToken());

            #endregion

            #region Assert

            Assert.True(response is ObjectResult);

            #endregion
        }
    }
}
