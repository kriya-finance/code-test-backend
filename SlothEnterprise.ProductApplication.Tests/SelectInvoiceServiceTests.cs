using System;
using FluentAssertions;
using Moq;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Abstractions;
using SlothEnterprise.ProductApplication.Abstractions.Services;
using SlothEnterprise.ProductApplication.Models.Applications;
using SlothEnterprise.ProductApplication.Models.Products;
using SlothEnterprise.ProductApplication.Services;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class SelectInvoiceServiceTests
    {
        private readonly IProductApplicationService _sut;
        private readonly Mock<ISelectInvoiceService> _selectInvoiceServiceMock = new Mock<ISelectInvoiceService>();

        public SelectInvoiceServiceTests()
        {
            _selectInvoiceServiceMock
                .Setup(m =>
                    m.SubmitApplicationFor(
                        It.IsNotNull<string>(),
                        It.IsAny<decimal>(),
                        It.IsAny<decimal>()))
                .Returns(1);

            _sut = new ProductApplicationService(
                _selectInvoiceServiceMock.Object,
                null,
                null);
        }

        [Fact]
        public void BusinessLoansService_WhenCalledWithCorrectRequest_ShouldReturnOne()
        {
            // GIVEN
            var sellerApplicationMock = new Mock<ISellerApplication>();
            sellerApplicationMock.SetupProperty(p =>
                p.Product, new SelectiveInvoiceDiscount { InvoiceAmount = 1000, AdvancePercentage = 10 });
            sellerApplicationMock.SetupProperty(p =>
                p.CompanyData, new SellerCompanyData { Founded = new DateTime(2000, 01, 20), Name = "MyOrg", Number = 1, DirectorName = "TestName" });

            // WHEN
            var result = _sut.SubmitApplicationFor(sellerApplicationMock.Object);

            //THEN
            result.Should().Be(1);
        }
    }
}
