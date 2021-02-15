using Moq;
using SlothEnterprise.ProductApplication.ApplicationHandlers;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationTests
    {
        private readonly Mock<IApplicationHandler> _selectInvoiceApplicationHandlerMock;
        private readonly Mock<IApplicationHandler> _confidentialInvoiceApplicationHandler;
        private readonly Mock<IApplicationHandler> _businessLoansApplicationHandlerMock;

        private readonly Mock<ISellerApplication> _sellerApplicationMock;

        private readonly IProductApplicationService _sut;

        public ProductApplicationTests()
        {
            _selectInvoiceApplicationHandlerMock = new Mock<IApplicationHandler>();
            _confidentialInvoiceApplicationHandler = new Mock<IApplicationHandler>();
            _businessLoansApplicationHandlerMock = new Mock<IApplicationHandler>();

            _selectInvoiceApplicationHandlerMock.Setup(x => x.CanHandle(It.IsAny<SelectiveInvoiceDiscount>())).Returns(true);
            _confidentialInvoiceApplicationHandler.Setup(x => x.CanHandle(It.IsAny<ConfidentialInvoiceDiscount>())).Returns(true);
            _businessLoansApplicationHandlerMock.Setup(x => x.CanHandle(It.IsAny<BusinessLoans>())).Returns(true);

            _sellerApplicationMock = new Mock<ISellerApplication>();

            _sut = new ProductApplicationService(
                new IApplicationHandler[]
                {
                    _selectInvoiceApplicationHandlerMock.Object,
                    _confidentialInvoiceApplicationHandler.Object,
                    _businessLoansApplicationHandlerMock.Object
                }
             );
        }

        [Fact]
        public void ProductApplicationService_WhenCalledWithSelectiveInvoiceDiscount_ShouldCallAppropriateProductHandler()
        {
            _sellerApplicationMock.SetupProperty(x => x.Product, new SelectiveInvoiceDiscount());

            _sut.SubmitApplicationFor(_sellerApplicationMock.Object);

            _selectInvoiceApplicationHandlerMock.Verify(x => x.Handle(_sellerApplicationMock.Object), Times.Once());
        }

        [Fact]
        public void ProductApplicationService_WhenCalledWithConfidentialInvoiceDiscount_ShouldCallAppropriateProductHandler()
        {
            _sellerApplicationMock.SetupProperty(x => x.Product, new ConfidentialInvoiceDiscount());

            _sut.SubmitApplicationFor(_sellerApplicationMock.Object);

            _confidentialInvoiceApplicationHandler.Verify(x => x.Handle(_sellerApplicationMock.Object), Times.Once());
        }

        [Fact]
        public void ProductApplicationService_WhenCalledWithBusinessLoans_ShouldCallAppropriateProductHandler()
        {
            _sellerApplicationMock.SetupProperty(x => x.Product, new BusinessLoans());

            _sut.SubmitApplicationFor(_sellerApplicationMock.Object);

            _businessLoansApplicationHandlerMock.Verify(x => x.Handle(_sellerApplicationMock.Object), Times.Once());
        }
    }
}