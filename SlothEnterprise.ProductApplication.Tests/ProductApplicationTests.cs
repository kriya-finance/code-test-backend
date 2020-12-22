using System;
using Moq;
using SlothEnterprise.ProductApplication.Abstractions;
using SlothEnterprise.ProductApplication.Abstractions.Services;
using SlothEnterprise.ProductApplication.Models.Applications;
using SlothEnterprise.ProductApplication.Models.Products;
using SlothEnterprise.ProductApplication.Services;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationTests
    {
        private readonly IProductApplicationService _sut;

        public ProductApplicationTests()
        {
            _sut = new ProductApplicationService(
                null,
                null,
                null);
        }

        [Fact]
        public void ProductApplicationTests_SubmitApplicationFor_WhenCalledWithMissingCompanyData_ShouldThrowArgumentNullException()
        {
            // GIVEN
            var sellerApplicationMock = new Mock<ISellerApplication>();
            sellerApplicationMock.SetupProperty(p => p.Product, new ConfidentialInvoiceDiscount { TotalLedgerNetworth = 100, AdvancePercentage = 10, VatRate = 0.15M });

            //THEN
            Assert.Throws<ArgumentNullException>(() => _sut.SubmitApplicationFor(sellerApplicationMock.Object));
        }

        [Fact]
        public void ProductApplicationTests_SubmitApplicationFor_WhenCalledWithMissingProduct_ShouldThrowArgumentNullException()
        {
            // GIVEN
            var sellerApplicationMock = new Mock<ISellerApplication>();
            sellerApplicationMock.SetupProperty(p => p.CompanyData, new SellerCompanyData());

            //THEN
            Assert.Throws<ArgumentNullException>(() => _sut.SubmitApplicationFor(sellerApplicationMock.Object));
        }
    }
}