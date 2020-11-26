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
    public class BusinessLoansHandlerTests
    {
        private readonly IProductApplicationHandler _sut;
        private readonly ISellerApplication _sellerApplication;
        private readonly Mock<IBusinessLoansService> _businessLoansServiceMock = new Mock<IBusinessLoansService>();
        private readonly Mock<IApplicationResult> _result = new Mock<IApplicationResult>();

        public BusinessLoansHandlerTests()
        {
            _sut = new BusinessLoansHandler(_businessLoansServiceMock.Object);

            _result.SetupProperty(p => p.ApplicationId, 3);
            _result.SetupProperty(p => p.Success, true);

            var sellerApplicationMock = new Mock<ISellerApplication>();
            sellerApplicationMock.SetupProperty(p => p.Product, new BusinessLoans());
            sellerApplicationMock.SetupProperty(p => p.CompanyData, new SellerCompanyData());
            _sellerApplication = sellerApplicationMock.Object;
        }

        [Fact]
        public void BusinessLoansHandler_ShouldReturnExpectedResults()
        {
            _businessLoansServiceMock.Setup(m => m.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<LoansRequest>())).Returns(_result.Object);
            var result = _sut.Handle(_sellerApplication);
            result.Should().Be(3);
        }

        [Fact]
        public void BusinessLoansHandler_CanHandleSelectiveInvoiceDiscount()
        {
            var result = _sut.CanHandle(_sellerApplication.Product);
            result.Should().Be(true);
        }

        [Fact]
        public void BusinessLoansHandler_ShouldNotHandlerOtherProductType()
        {
            var result = _sut.CanHandle(new ConfidentialInvoiceDiscount() { });
            result.Should().Be(false);
        }

        [Fact]
        public void BusinessLoansHandler_ShouldNotHandleNull()
        {
            var result = _sut.CanHandle(null);
            result.Should().Be(false);
        }

        [Fact]
        public void BusinessLoansHandler_ShouldThrowIfApplicationIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.Handle(null));
        }

        [Fact]
        public void BusinessLoansHandler_NullConstructorParameters_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new BusinessLoansHandler(null));
        }

    }
}