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
using SlothEnterprise.ProductApplication.Services.ProductServices;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class BusinessLoansServiceTests
    {
        private readonly IProductApplicationService _sut;
        private readonly Mock<IBusinessLoansService> _businessLoansServiceMock = new Mock<IBusinessLoansService>();

        public BusinessLoansServiceTests()
        {
            var successAppResult = new Mock<IApplicationResult>();
            successAppResult.SetupProperty(p => p.ApplicationId, 1);
            successAppResult.SetupProperty(p => p.Success, true);

            _businessLoansServiceMock
                .Setup(m =>
                    m.SubmitApplicationFor(
                        It.IsNotNull<CompanyDataRequest>(),
                        It.IsNotNull<LoansRequest>()))
                .Returns(successAppResult.Object);

            var service = new BusinessLoansService(_businessLoansServiceMock.Object);
            var serviceRegistry = new ProductServiceProvider();
            serviceRegistry.RegisterService(service);
            _sut = new ProductApplicationService(serviceRegistry);
        }

        [Fact]
        public void BusinessLoansService_WhenCalledWithCorrectRequest_ShouldReturnOne()
        {
            // GIVEN
            var sellerApplicationMock = new Mock<ISellerApplication>();
            sellerApplicationMock.SetupProperty(p =>
                p.Product, new BusinessLoans { LoanAmount = 1000, InterestRatePerAnnum = 0.1M });
            sellerApplicationMock.SetupProperty(p =>
                p.CompanyData, new SellerCompanyData { Founded = new DateTime(2000, 01, 20), Name = "MyOrg", Number = 1, DirectorName = "TestName" });

            // WHEN
            var result = _sut.SubmitApplicationFor(sellerApplicationMock.Object);

            //THEN
            result.Should().Be(1);
        }
    }
}