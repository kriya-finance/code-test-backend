using System;
using FluentAssertions;
using Moq;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Abstractions;
using SlothEnterprise.ProductApplication.Abstractions.Services;
using SlothEnterprise.ProductApplication.Models.Applications;
using SlothEnterprise.ProductApplication.Models.Products;
using SlothEnterprise.ProductApplication.Services;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ConfidentialInvoiceServiceTests
    {
        private readonly IProductApplicationService _sut;
        private readonly Mock<IConfidentialInvoiceService> _confidentialInvoiceServiceMock = new Mock<IConfidentialInvoiceService>();

        public ConfidentialInvoiceServiceTests()
        {
            var successAppResult = new Mock<IApplicationResult>();
            successAppResult.SetupProperty(p => p.ApplicationId, 1);
            successAppResult.SetupProperty(p => p.Success, true);

            _confidentialInvoiceServiceMock
                .Setup(m =>
                    m.SubmitApplicationFor(
                        It.IsNotNull<CompanyDataRequest>(),
                        It.IsAny<decimal>(),
                        It.IsAny<decimal>(),
                        It.IsAny<decimal>()))
                .Returns(successAppResult.Object);

            _sut = new ProductApplicationService(
                null,
                _confidentialInvoiceServiceMock.Object,
                null);
        }

        [Fact]
        public void ConfidentialInvoiceService_WhenCalledWithCorrectRequest_ShouldReturnOne()
        {
            // GIVEN
            var sellerApplicationMock = new Mock<ISellerApplication>();
            sellerApplicationMock.SetupProperty(p =>
                p.Product, new ConfidentialInvoiceDiscount { TotalLedgerNetworth = 100, AdvancePercentage = 10, VatRate = 0.15M });
            sellerApplicationMock.SetupProperty(p =>
                p.CompanyData, new SellerCompanyData { Founded = new DateTime(2000, 01, 20), Name = "MyOrg", Number = 1, DirectorName = "TestName" });

            // WHEN
            var result = _sut.SubmitApplicationFor(sellerApplicationMock.Object);

            //THEN
            result.Should().Be(1);
        }
    }
}