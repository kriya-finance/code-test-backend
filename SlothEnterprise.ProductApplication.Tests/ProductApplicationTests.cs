using FluentAssertions;
using Moq;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Handlers;
using SlothEnterprise.ProductApplication.Products;
using System;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationTests
    {
        private readonly ProductApplicationService _sut;
        private readonly Mock<IProductApplicationMediatorService> _mediatorService = new Mock<IProductApplicationMediatorService>();
        private readonly Mock<IConfidentialInvoiceService> _confidentialInvoiceServiceMock = new Mock<IConfidentialInvoiceService>();
        private readonly Mock<IBusinessLoansService> _businessLoansServiceMock = new Mock<IBusinessLoansService>();
        private readonly ISellerApplication _sellerApplication;
        private readonly int _result = 10;

        public ProductApplicationTests()
        {
            _sut = new ProductApplicationService(_mediatorService.Object);

            var sellerApplicationMock = new Mock<ISellerApplication>();
            sellerApplicationMock.SetupProperty(p => p.Product, new ConfidentialInvoiceDiscount());
            sellerApplicationMock.SetupProperty(p => p.CompanyData, new SellerCompanyData());
            _sellerApplication = sellerApplicationMock.Object;
        }

        [Fact]
        public void ProductApplicationService_SubmitApplicationFor_WhenCalled_ShouldCallMediatorSubmitApplicationForOnce()
        {
            _mediatorService.Setup(m => m.SubmitApplicationFor(It.IsAny<ISellerApplication>())).Returns(_result);
            var result = _sut.SubmitApplicationFor(_sellerApplication);
            result.Should().Be(_result);
        }

        [Fact]
        public void ProductApplicationService_NullConstructorParameters_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new ProductApplicationService(null));
        }
    }
}