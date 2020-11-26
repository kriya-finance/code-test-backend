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
    public class SelectiveInvoiceDiscountHandlerTests
    {
        private readonly IProductApplicationHandler _sut;
        private readonly ISellerApplication _sellerApplication;
        private readonly Mock<ISelectInvoiceService> _selectInvoiceServiceMock = new Mock<ISelectInvoiceService>();
        private readonly int _result = 2;

        public SelectiveInvoiceDiscountHandlerTests()
        {
            _sut = new SelectiveInvoiceDiscountHandler(_selectInvoiceServiceMock.Object);

            var sellerApplicationMock = new Mock<ISellerApplication>();
            sellerApplicationMock.SetupProperty(p => p.Product, new SelectiveInvoiceDiscount());
            sellerApplicationMock.SetupProperty(p => p.CompanyData, new SellerCompanyData());
            _sellerApplication = sellerApplicationMock.Object;
        }

        [Fact]
        public void SelectiveInvoiceDiscountHandler_ShouldReturnExpectedResults()
        {
            _selectInvoiceServiceMock.Setup(m => m.SubmitApplicationFor(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(_result);
            var result = _sut.Handle(_sellerApplication);
            result.Should().Be(_result);
        }

        [Fact]
        public void SelectiveInvoiceDiscountHandler_CanHandleSelectiveInvoiceDiscount()
        {
            var result = _sut.CanHandle(_sellerApplication.Product);
            result.Should().Be(true);
        }

        [Fact]
        public void SelectiveInvoiceDiscountHandler_ShouldNotHandlerOtherProductType()
        {
            var result = _sut.CanHandle(new BusinessLoans() { });
            result.Should().Be(false);
        }

        [Fact]
        public void SelectiveInvoiceDiscountHandler_ShouldNotHandleNull()
        {
            var result = _sut.CanHandle(null);
            result.Should().Be(false);
        }

        [Fact]
        public void SelectiveInvoiceDiscountHandler_ShouldThrowIfApplicationIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.Handle(null));
        }

        [Fact]
        public void SelectiveInvoiceDiscountHandler_NullConstructorParameters_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new SelectiveInvoiceDiscountHandler(null));
        }
    }
}