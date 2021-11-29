using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentsAPI.Application.CQRS.Commands;
using PaymentsAPI.Application.CQRS.Queries;
using PaymentsAPI.Application.Dtos;
using PaymentsAPI.Controllers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PaymentsUnitTests.API.Controllers
{
    public class PaymentsControllerTests
    {
        private readonly PaymentsController _paymentsController;
        private readonly Mock<IMediator> _mockMediator;

        public PaymentsControllerTests()
        {
            var mockLogger = new Mock<ILogger<PaymentsController>>();
            _mockMediator = new Mock<IMediator>();

            _paymentsController = new PaymentsController(mockLogger.Object, _mockMediator.Object);
        }

        #region GetById

        [Fact]
        [Trait("Category", "GetById")]
        public async Task GetById_ResultNotNull_Test()
        {
            #region Arrange

            _mockMediator.Setup(s => s.Send(It.IsAny<GetPaymentQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ObjectResult(new PaymentDto()));

            #endregion

            #region Act

            var response = await _paymentsController.GetById(1);

            #endregion

            #region Assert

            Assert.NotNull(response);

            #endregion
        }

        [Fact]
        [Trait("Category", "GetById")]
        public async Task GetById_ResultTypeOfObjectResult_Test()
        {
            #region Arrange

            _mockMediator.Setup(s => s.Send(It.IsAny<GetPaymentQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ObjectResult(new PaymentDto()));

            #endregion

            #region Act

            var response = await _paymentsController.GetById(1);

            #endregion

            #region Assert

            Assert.IsType<ObjectResult>(response);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ControllersTestData.HandlerResponseOk<PaymentDto>))]
        [Trait("Category", "GetById")]
        public async Task GetById_ResultOk_Test(int statusCode, PaymentDto paymentDto)
        {
            #region Arrange

            _mockMediator.Setup(s =>
                   s.Send(It.IsAny<GetPaymentQuery>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(new ObjectResult(paymentDto) { StatusCode = statusCode });

            #endregion

            #region Act

            var response = await _paymentsController.GetById(1);

            var objectResult = response as ObjectResult;

            #endregion

            #region Assert

            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ControllersTestData.HandlerResponseOk<PaymentDto>))]
        [Trait("Category", "GetById")]
        public async Task GetById_ResultOkDataTypeOfPaymentDto_Test(int statusCode, PaymentDto paymentDto)
        {
            #region Arrange

            _mockMediator.Setup(s =>
                   s.Send(It.IsAny<GetPaymentQuery>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(new ObjectResult(paymentDto) { StatusCode = statusCode });

            #endregion

            #region Act

            var response = await _paymentsController.GetById(1);

            var objectResult = response as ObjectResult;

            #endregion

            #region Assert

            Assert.IsType<PaymentDto>(objectResult.Value);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ControllersTestData.HandlerResponseNotFound))]
        [Trait("Category", "GetById")]
        public async Task GetById_ResultNotFound_Test(int statusCode)
        {
            #region Arrange

            _mockMediator.Setup(s =>
                    s.Send(It.IsAny<GetPaymentQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ObjectResult(null) { StatusCode = statusCode });

            #endregion

            #region Act

            var response = await _paymentsController.GetById(1);

            var objectResult = response as ObjectResult;

            #endregion

            #region Assert

            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);

            #endregion
        }

        #endregion

        #region Get

        [Fact]
        [Trait("Category", "Get")]
        public async Task Get_ResultNotNull_Test()
        {
            #region Arrange

            _mockMediator.Setup(s =>
                    s.Send(It.IsAny<GetPaymentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ObjectResult(new List<PaymentDto>()));

            #endregion

            #region Act

            var response = await _paymentsController.Get();

            #endregion

            #region Assert

            Assert.NotNull(response);

            #endregion
        }

        [Fact]
        [Trait("Category", "Get")]
        public async Task Get_ResultTypeOfObjectResult_Test()
        {
            #region Arrange

            _mockMediator.Setup(s =>
                    s.Send(It.IsAny<GetPaymentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ObjectResult(new List<PaymentDto>()));

            #endregion

            #region Act

            var response = await _paymentsController.Get();

            #endregion

            #region Assert

            Assert.IsType<ObjectResult>(response);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ControllersTestData.HandlerResponseOk<List<PaymentDto>>))]
        [Trait("Category", "Get")]
        public async Task Get_ResultOk_Test(int statusCode, List<PaymentDto> paymentsDto)
        {
            #region Arrange

            _mockMediator.Setup(s =>
                    s.Send(It.IsAny<GetPaymentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ObjectResult(paymentsDto) { StatusCode = statusCode });

            #endregion

            #region Act

            var response = await _paymentsController.Get();

            var objectResult = response as ObjectResult;

            #endregion

            #region Assert

            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ControllersTestData.HandlerResponseOk<List<PaymentDto>>))]
        [Trait("Category", "Get")]
        public async Task Get_ResultOkDataTypeOfListPaymentDto_Test(int statusCode, List<PaymentDto> paymentsDto)
        {
            #region Arrange

            _mockMediator.Setup(s =>
                    s.Send(It.IsAny<GetPaymentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ObjectResult(paymentsDto) { StatusCode = statusCode });

            #endregion

            #region Act

            var response = await _paymentsController.Get();

            var objectResult = response as ObjectResult;

            #endregion

            #region Assert

            Assert.IsType<List<PaymentDto>>(objectResult.Value);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ControllersTestData.HandlerResponseNoContent))]
        [Trait("Category", "Get")]
        public async Task Get_ResultNoContent_Test(int statusCode)
        {
            #region Arrange

            _mockMediator.Setup(s =>
                    s.Send(It.IsAny<GetPaymentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ObjectResult(null) { StatusCode = statusCode });

            #endregion

            #region Act

            var response = await _paymentsController.Get();

            var objectResult = response as ObjectResult;

            #endregion

            #region Assert

            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);

            #endregion
        }

        #endregion

        #region Create

        [Fact]
        [Trait("Category", "Create")]
        public async Task Create_ResultNotNull_Test()
        {
            #region Arrange

            _mockMediator.Setup(s =>
                    s.Send(It.IsAny<CreatePaymentCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ObjectResult(new PaymentDto()));

            #endregion

            #region Act

            var response = await _paymentsController.Create(new CreatePaymentCommand());

            #endregion

            #region Assert

            Assert.NotNull(response);

            #endregion
        }

        [Fact]
        [Trait("Category", "Create")]
        public async Task Create_ResultTypeOfObjectResult_Test()
        {
            #region Arrange

            _mockMediator.Setup(s =>
                    s.Send(It.IsAny<CreatePaymentCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ObjectResult(new PaymentDto()));

            #endregion

            #region Act

            var response = await _paymentsController.Create(new CreatePaymentCommand());

            #endregion

            #region Assert

            Assert.IsType<ObjectResult>(response);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ControllersTestData.HandlerResponseCreated<PaymentDto>))]
        [Trait("Category", "Create")]
        public async Task Create_ResultCreated_Test(int statusCode, PaymentDto paymentDto)
        {
            #region Arrange

            _mockMediator.Setup(s =>
                    s.Send(It.IsAny<CreatePaymentCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ObjectResult(paymentDto) { StatusCode = statusCode });

            #endregion

            #region Act

            var response = await _paymentsController.Create(new CreatePaymentCommand());

            var objectResult = response as ObjectResult;

            #endregion

            #region Assert

            Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ControllersTestData.HandlerResponseCreated<PaymentDto>))]
        [Trait("Category", "Create")]
        public async Task CreateAbsence_ResultCreatedDataTypeOfPaymentDto_Test(int statusCode, PaymentDto paymentDto)
        {
            #region Arrange

            _mockMediator.Setup(s =>
                    s.Send(It.IsAny<CreatePaymentCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ObjectResult(paymentDto) { StatusCode = statusCode });

            #endregion

            #region Act

            var response = await _paymentsController.Create(new CreatePaymentCommand());

            var objectResult = response as ObjectResult;

            #endregion

            #region Assert

            Assert.IsType<PaymentDto>(objectResult.Value);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ControllersTestData.HandlerResponseBadRequest))]
        [Trait("Category", "Create")]
        public async Task Create_ResultBadRequest_Test(int statusCode)
        {
            #region Arrange

            _mockMediator.Setup(s =>
                    s.Send(It.IsAny<CreatePaymentCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ObjectResult(null) { StatusCode = statusCode });

            #endregion

            #region Act

            var response = await _paymentsController.Create(new CreatePaymentCommand());

            var objectResult = response as ObjectResult;

            #endregion

            #region Assert

            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);

            #endregion
        }

        #endregion
    }
}
