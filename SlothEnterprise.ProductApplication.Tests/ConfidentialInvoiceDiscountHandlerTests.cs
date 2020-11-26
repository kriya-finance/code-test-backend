using FluentAssertions;
using Moq;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Handlers;
using SlothEnterprise.ProductApplication.Products;
using System;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ConfidentialInvoiceDiscountHandlerTests
    {
        private readonly IProductApplicationHandler _sut;
        private readonly ISellerApplication _sellerApplication;
        private readonly Mock<IConfidentialInvoiceService> _confidentialInvoiceServiceMock = new Mock<IConfidentialInvoiceService>();
        private readonly Mock<IApplicationResult> _result = new Mock<IApplicationResult>();

        public ConfidentialInvoiceDiscountHandlerTests()
        {
            _sut = new ConfidentialInvoiceDiscountHandler(_confidentialInvoiceServiceMock.Object);

            _result.SetupProperty(p => p.ApplicationId, 1);
            _result.SetupProperty(p => p.Success, true);

            var sellerApplicationMock = new Mock<ISellerApplication>();
            sellerApplicationMock.SetupProperty(p => p.Product, new ConfidentialInvoiceDiscount());
            sellerApplicationMock.SetupProperty(p => p.CompanyData, new SellerCompanyData());
            _sellerApplication = sellerApplicationMock.Object;
        }

        [Fact]
        public void ConfidentialInvoiceDiscountHandler_ShouldReturnExpectedResults()
        {
            _confidentialInvoiceServiceMock.Setup(m => m.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(_result.Object);
            var result = _sut.Handle(_sellerApplication);
            result.Should().Be(1);
        }

        [Fact]
        public void ConfidentialInvoiceDiscountHandler_CanHandleSelectiveInvoiceDiscount()
        {
            var result = _sut.CanHandle(_sellerApplication.Product);
            result.Should().Be(true);
        }

        [Fact]
        public void ConfidentialInvoiceDiscountHandler_ShouldNotHandlerOtherProductType()
        {
            var result = _sut.CanHandle(new BusinessLoans() { });
            result.Should().Be(false);
        }

        [Fact]
        public void ConfidentialInvoiceDiscountHandler_ShouldNotHandleNull()
        {
            var result = _sut.CanHandle(null);
            result.Should().Be(false);
        }

        [Fact]
        public void ConfidentialInvoiceDiscountHandler_ShouldThrowIfApplicationIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.Handle(null));
        }

        [Fact]
        public void ConfidentialInvoiceDiscountHandler_NullConstructorParameters_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new SelectiveInvoiceDiscountHandler(null));
        }
    }
}