using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentsAPI.Application.Models;
using PaymentsAPI.Services;
using PaymentsDomain.AggregatesModel.PaymentAggregate;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PaymentsUnitTests.API.Services
{
    public class PaymentsServiceTests
    {
        private readonly Mock<IPaymentsRepository> _mockIPaymentsRepository;
        private readonly PaymentsService _paymentsService;

        public PaymentsServiceTests()
        {
            var mockLogger = new Mock<ILogger<PaymentsService>>();

            _mockIPaymentsRepository = new Mock<IPaymentsRepository>();

            _paymentsService = new PaymentsService(mockLogger.Object, _mockIPaymentsRepository.Object);
        }

        [Fact]
        [Trait("Category", "GetById")]
        public async Task GetById_ResultNotNull_Test()
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.FindById(It.IsAny<int>()))
                .ReturnsAsync(new Payment());

            #endregion

            #region Act

            var response = await _paymentsService.Get(1);

            #endregion

            #region Assert

            Assert.NotNull(response);

            #endregion
        }

        [Fact]
        [Trait("Category", "GetById")]
        public async Task GetById_ResultTypeOfServiceResponsePayment_Test()
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.FindById(It.IsAny<int>()))
                .ReturnsAsync(new Payment());

            #endregion

            #region Act

            var response = await _paymentsService.Get(1);

            #endregion

            #region Assert

            Assert.IsType<ServiceResponse<Payment>>(response);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.HandlerResponseOk<Payment>))]
        [Trait("Category", "GetById")]
        public async Task GetById_ResultTypeOfServiceResponsePayment_WithStatusCodeOK_Test(Payment payment)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.FindById(It.IsAny<int>()))
                .ReturnsAsync(payment);

            #endregion

            #region Act

            var response = await _paymentsService.Get(1);

            #endregion

            #region Assert

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.HandlerResponseOk<Payment>))]
        [Trait("Category", "GetById")]
        public async Task GetById_ResultTypeOfServiceResponsePayment_WithStatusCodeOK_SuccessTrue_Test(Payment payment)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.FindById(It.IsAny<int>()))
                .ReturnsAsync(payment);

            #endregion

            #region Act

            var response = await _paymentsService.Get(1);

            #endregion

            #region Assert

            Assert.True(response.Success);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.HandlerResponseOk<Payment>))]
        [Trait("Category", "GetById")]
        public async Task GetById_ResultTypeOfServiceResponsePayment_WithStatusCodeOK_DataNotNull_Test(Payment payment)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.FindById(It.IsAny<int>()))
                .ReturnsAsync(payment);

            #endregion

            #region Act

            var response = await _paymentsService.Get(1);

            #endregion

            #region Assert

            Assert.NotNull(response.Data);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.NullObject))]
        [Trait("Category", "GetById")]
        public async Task GetById_ResultTypeOfServiceResponsePayment_WithStatusCodeNotFound_Test(Payment payment)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.FindById(It.IsAny<int>()))
                .ReturnsAsync(payment);

            #endregion

            #region Act

            var response = await _paymentsService.Get(1);

            #endregion

            #region Assert

            Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.NullObject))]
        [Trait("Category", "GetById")]
        public async Task GetById_ResultTypeOfServiceResponsePayment_WithStatusCodeNotFound_SuccessFalse_Test(Payment payment)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.FindById(It.IsAny<int>()))
                .ReturnsAsync(payment);

            #endregion

            #region Act

            var response = await _paymentsService.Get(1);

            #endregion

            #region Assert

            Assert.False(response.Success);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.NullObject))]
        [Trait("Category", "GetById")]
        public async Task GetById_ResultTypeOfServiceResponsePayment_WithStatusCodeNotFound_DataNull_Test(Payment payment)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.FindById(It.IsAny<int>()))
                .ReturnsAsync(payment);

            #endregion

            #region Act

            var response = await _paymentsService.Get(1);

            #endregion

            #region Assert

            Assert.Null(response.Data);

            #endregion
        }

        [Fact]
        [Trait("Category", "Get")]
        public async Task Get_ResultNotNull_Test()
        {
            #region Arrange

            var payments = new List<Payment> { new Payment() };

            _mockIPaymentsRepository.Setup(s => s.FindAll())
                .ReturnsAsync(payments);

            #endregion

            #region Act

            var response = await _paymentsService.Get();

            #endregion

            #region Assert

            Assert.NotNull(response);

            #endregion
        }

        [Fact]
        [Trait("Category", "Get")]
        public async Task Get_ResultTypeOfServiceResponseListPayment_Test()
        {
            #region Arrange

            var payments = new List<Payment> { new Payment() };

            _mockIPaymentsRepository.Setup(s => s.FindAll())
                .ReturnsAsync(payments);

            #endregion

            #region Act

            var response = await _paymentsService.Get();

            #endregion

            #region Assert

            Assert.IsType<ServiceResponse<IEnumerable<Payment>>>(response);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.NoContent<Payment>))]
        [Trait("Category", "Get")]
        public async Task Get_ResultTypeOfServiceResponseListPayment_WithStatusCodeNoContent_Test(List<Payment> payments)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.FindAll())
               .ReturnsAsync(payments);

            #endregion

            #region Act

            var response = await _paymentsService.Get();

            #endregion

            #region Assert

            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.NoContent<Payment>))]
        [Trait("Category", "Get")]
        public async Task Get_ResultTypeOfServiceResponseListPayment_WithStatusCodeNoContent_SuccessTrue_Test(List<Payment> payments)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.FindAll())
               .ReturnsAsync(payments);

            #endregion

            #region Act

            var response = await _paymentsService.Get();

            #endregion

            #region Assert

            Assert.True(response.Success);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.NoContent<Payment>))]
        [Trait("Category", "Get")]
        public async Task Get_ResultTypeOfServiceResponseListPayment_WithStatusCodeNoContent_DataNull_Test(List<Payment> payments)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.FindAll())
               .ReturnsAsync(payments);

            #endregion

            #region Act

            var response = await _paymentsService.Get();

            #endregion

            #region Assert

            Assert.Null(response.Data);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.CollectionOf<Payment>))]
        [Trait("Category", "Get")]
        public async Task Get_ResultTypeOfServiceResponseListPayment_WithStatusCodeOK_Test(List<Payment> payments)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.FindAll())
               .ReturnsAsync(payments);

            #endregion

            #region Act

            var response = await _paymentsService.Get();

            #endregion

            #region Assert

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.CollectionOf<Payment>))]
        [Trait("Category", "Get")]
        public async Task Get_ResultTypeOfServiceResponseListPayment_WithStatusCodeOK_SuccessTrue_Test(List<Payment> payments)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.FindAll())
               .ReturnsAsync(payments);

            #endregion

            #region Act

            var response = await _paymentsService.Get();

            #endregion

            #region Assert

            Assert.True(response.Success);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.CollectionOf<Payment>))]
        [Trait("Category", "Get")]
        public async Task Get_ResultTypeOfServiceResponseListPayment_WithStatusCodeOK_DataNotNull_Test(List<Payment> payments)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.FindAll())
               .ReturnsAsync(payments);

            #endregion

            #region Act

            var response = await _paymentsService.Get();

            #endregion

            #region Assert

            Assert.NotNull(response.Data);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.CollectionOf<Payment>))]
        [Trait("Category", "Get")]
        public async Task Get_ResultTypeOfServiceResponseListPayment_WithStatusCodeOK_DataTypeOfListPayment_Test(List<Payment> payments)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.FindAll())
               .ReturnsAsync(payments);

            #endregion

            #region Act

            var response = await _paymentsService.Get();

            #endregion

            #region Assert

            Assert.IsAssignableFrom<IEnumerable<Payment>>(response.Data);

            #endregion
        }

        [Fact]
        [Trait("Category", "SavePayment")]
        public async Task SavePayment_ResultNotNull_Test()
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.Update(It.IsAny<Payment>()));

            #endregion

            #region Act

            var response = await _paymentsService.SavePayment(new Payment());

            #endregion

            #region Assert

            Assert.NotNull(response);

            #endregion
        }

        [Fact]
        [Trait("Category", "SavePayment")]
        public async Task SavePayment_ResultTypeOfServiceResponseBool_Test()
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.Update(It.IsAny<Payment>()));

            #endregion

            #region Act

            var response = await _paymentsService.SavePayment(new Payment());

            #endregion

            #region Assert

            Assert.IsType<ServiceResponse<bool?>>(response);

            #endregion
        }

        [Fact]
        [Trait("Category", "SavePayment")]
        public async Task SavePaymentr_ResultTypeOfServiceResponseBool_WithStatusCodeOK_Test()
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.Update(It.IsAny<Payment>()));

            #endregion

            #region Act

            var response = await _paymentsService.SavePayment(new Payment());

            #endregion

            #region Assert

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);

            #endregion
        }

        [Fact]
        [Trait("Category", "SavePayment")]
        public async Task SavePayment_ResultTypeOfServiceResponseBool_WithStatusCodeOK_SuccessTrue_Test()
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.Update(It.IsAny<Payment>()));

            #endregion

            #region Act

            var response = await _paymentsService.SavePayment(new Payment());

            #endregion

            #region Assert

            Assert.True(response.Success);

            #endregion
        }

        [Fact]
        [Trait("Category", "SavePayment")]
        public async Task SavePayment_ResultTypeOfServiceResponseBool_WithStatusCodeOK_DataNull_Test()
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.Update(It.IsAny<Payment>()));

            #endregion

            #region Act

            var response = await _paymentsService.SavePayment(new Payment());

            #endregion

            #region Assert

            Assert.Null(response.Data);

            #endregion
        }

        [Fact]
        [Trait("Category", "Save")]
        public async Task Save_ResultNotNull_Test()
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.UnitOfWork.SaveEntitiesAsync(new CancellationToken()))
                .ReturnsAsync(true);

            #endregion

            #region Act

            var response = await _paymentsService.Save(new CancellationToken());

            #endregion

            #region Assert

            Assert.NotNull(response);

            #endregion
        }

        [Fact]
        [Trait("Category", "Save")]
        public async Task Save_ResultTypeOfServiceResponseBool_Test()
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.UnitOfWork.SaveEntitiesAsync(new CancellationToken()))
                .ReturnsAsync(true);

            #endregion

            #region Act

            var response = await _paymentsService.Save(new CancellationToken());

            #endregion

            #region Assert

            Assert.IsType<ServiceResponse<bool?>>(response);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.CancellationTk))]
        [Trait("Category", "Save")]
        public async Task Save_ResultTypeOfServiceResponseBool_WithStatusCodeOK_Test(CancellationToken cancellationToken)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.UnitOfWork.SaveEntitiesAsync(cancellationToken))
                .ReturnsAsync(true);

            #endregion

            #region Act

            var response = await _paymentsService.Save(new CancellationToken());

            #endregion

            #region Assert

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.CancellationTk))]
        [Trait("Category", "Save")]
        public async Task Save_ResultTypeOfServiceResponseBool_WithStatusCodeOK_SuccessTrue_Test(CancellationToken cancellationToken)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.UnitOfWork.SaveEntitiesAsync(cancellationToken))
                .ReturnsAsync(true);

            #endregion

            #region Act

            var response = await _paymentsService.Save(new CancellationToken());

            #endregion

            #region Assert

            Assert.True(response.Success);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.CancellationTk))]
        [Trait("Category", "Save")]
        public async Task Save_ResultTypeOfServiceResponseBool_WithStatusCodeOK_DataIsNull_Test(CancellationToken cancellationToken)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.UnitOfWork.SaveEntitiesAsync(cancellationToken))
                .ReturnsAsync(true);

            #endregion

            #region Act

            var response = await _paymentsService.Save(new CancellationToken());

            #endregion

            #region Assert

            Assert.Null(response.Data);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.CancellationTk))]
        [Trait("Category", "Save")]
        public async Task Save_ResultTypeOfServiceResponseBool_WithStatusCodeInternalServerError_Test(CancellationToken cancellationToken)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.UnitOfWork.SaveEntitiesAsync(cancellationToken))
                .ReturnsAsync(false);

            #endregion

            #region Act

            var response = await _paymentsService.Save(new CancellationToken());

            #endregion

            #region Assert

            Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.CancellationTk))]
        [Trait("Category", "Save")]
        public async Task Save_ResultTypeOfServiceResponseBool_WithStatusCodeInternalServerError_SuccessFalse_Test(CancellationToken cancellationToken)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.UnitOfWork.SaveEntitiesAsync(cancellationToken))
                .ReturnsAsync(false);

            #endregion

            #region Act

            var response = await _paymentsService.Save(new CancellationToken());

            #endregion

            #region Assert

            Assert.False(response.Success);

            #endregion
        }

        [Theory]
        [ClassData(typeof(ServicesTestData.CancellationTk))]
        [Trait("Category", "Save")]
        public async Task Save_ResultTypeOfServiceResponseBool_WithStatusCodeInternalServerError_DataIsNull_Test(CancellationToken cancellationToken)
        {
            #region Arrange

            _mockIPaymentsRepository.Setup(s => s.UnitOfWork.SaveEntitiesAsync(cancellationToken))
                .ReturnsAsync(false);

            #endregion

            #region Act

            var response = await _paymentsService.Save(new CancellationToken());

            #endregion

            #region Assert

            Assert.Null(response.Data);

            #endregion
        }

    }
}
