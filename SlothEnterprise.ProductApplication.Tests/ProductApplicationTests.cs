using AutoFixture;
using NSubstitute;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationTests
    {
        private readonly Fixture _fixture;
        public ProductApplicationTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void SubmitApplicationFor_SelectInvoiceDiscount_CallsSelectInvoiceService()
        {
            var sid = Substitute.For<ISelectInvoiceService>();
            var cid = Substitute.For<IConfidentialInvoiceService>();
            var loans = Substitute.For<IBusinessLoansService>();

            var sidProduct = _fixture.Build<SelectiveInvoiceDiscount>().Create();
            var companyData = _fixture.Build<SellerCompanyData>().Create();
            var application = _fixture.Build<SellerApplication>()
                .With(a => a.Product, sidProduct)
                .With(a => a.CompanyData, companyData)
                .Create();

            sid.SubmitApplicationFor(application.CompanyData.Number.ToString(), sidProduct.InvoiceAmount, sidProduct.AdvancePercentage).Returns(100);

            var service = new ProductApplicationService(sid, cid, loans);
            var result = service.SubmitApplicationFor(application);

            sid.Received(1).SubmitApplicationFor(application.CompanyData.Number.ToString(), sidProduct.InvoiceAmount, sidProduct.AdvancePercentage);
            Assert.Equal(100, result);
        }
    }
}
