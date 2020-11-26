using Moq;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Exceptions;
using SlothEnterprise.ProductApplication.Handlers;
using SlothEnterprise.ProductApplication.Products;
using System;
using System.Collections.Generic;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationMediatorServiceTests
    {
        private readonly IProductApplicationMediatorService _sut;
        private readonly Mock<IProductApplicationHandler> _selectiveInvoiceDiscountHandler;
        private readonly Mock<IProductApplicationHandler> _confidentialInvoiceDiscountHandler;
        private readonly Mock<IProductApplicationHandler> _businessLoansHandler;

        private Mock<ISellerApplication> _sellerApplication;

        public ProductApplicationMediatorServiceTests()
        {
            _selectiveInvoiceDiscountHandler = new Mock<IProductApplicationHandler>();
            _confidentialInvoiceDiscountHandler = new Mock<IProductApplicationHandler>();
            _businessLoansHandler = new Mock<IProductApplicationHandler>();

            _selectiveInvoiceDiscountHandler.Setup(m => m.CanHandle(It.IsAny<SelectiveInvoiceDiscount>())).Returns(true);
            _confidentialInvoiceDiscountHandler.Setup(m => m.CanHandle(It.IsAny<ConfidentialInvoiceDiscount>())).Returns(true);
            _businessLoansHandler.Setup(m => m.CanHandle(It.IsAny<BusinessLoans>())).Returns(true);

            _sellerApplication = new Mock<ISellerApplication>();
            _sellerApplication.SetupProperty(p => p.CompanyData, new SellerCompanyData());

            _sut = new ProductApplicationMediatorService(new List<IProductApplicationHandler> { _selectiveInvoiceDiscountHandler.Object, _confidentialInvoiceDiscountHandler.Object, _businessLoansHandler.Object });
        }

        [Fact]
        public void ProductApplicationMediatorService_ForSelectiveInvoiceDiscount_ShouldCallCorrectHandler()
        {
            //arrange
            _sellerApplication.SetupProperty(p => p.Product, new SelectiveInvoiceDiscount());

            //act
            _sut.SubmitApplicationFor(_sellerApplication.Object);

            //assert
            _selectiveInvoiceDiscountHandler.Verify(x => x.Handle(_sellerApplication.Object), Times.Once());
            _confidentialInvoiceDiscountHandler.Verify(x => x.Handle(_sellerApplication.Object), Times.Never());
            _businessLoansHandler.Verify(x => x.Handle(_sellerApplication.Object), Times.Never());
        }

        [Fact]
        public void ProductApplicationMediatorService_ForBusinessLoans_ShouldCallCorrectHandler()
        {
            //arrange
            _sellerApplication.SetupProperty(p => p.Product, new BusinessLoans());

            //act
            _sut.SubmitApplicationFor(_sellerApplication.Object);

            //assert
            _selectiveInvoiceDiscountHandler.Verify(x => x.Handle(_sellerApplication.Object), Times.Never());
            _confidentialInvoiceDiscountHandler.Verify(x => x.Handle(_sellerApplication.Object), Times.Never());
            _businessLoansHandler.Verify(x => x.Handle(_sellerApplication.Object), Times.Once());
        }

        [Fact]
        public void ProductApplicationMediatorService_ForConfidentialInvoiceDiscount_ShouldCallCorrectHandler()
        {
            //arrange
            _sellerApplication.SetupProperty(p => p.Product, new ConfidentialInvoiceDiscount());

            //act
            _sut.SubmitApplicationFor(_sellerApplication.Object);

            //assert
            _selectiveInvoiceDiscountHandler.Verify(x => x.Handle(_sellerApplication.Object), Times.Never());
            _confidentialInvoiceDiscountHandler.Verify(x => x.Handle(_sellerApplication.Object), Times.Once());
            _businessLoansHandler.Verify(x => x.Handle(_sellerApplication.Object), Times.Never());
        }

        [Fact]
        public void ProductApplicationMediatorService_ForProductWithoutHandler_ShouldThrow()
        {
            //arrange
            _sellerApplication.SetupProperty(p => p.Product, new ProductWithoutHandler());

            //assert
            Assert.Throws<InvalidOperationException>(() => _sut.SubmitApplicationFor(_sellerApplication.Object));
        }

        [Fact]
        public void ProductApplicationMediatorService_NullApplication_ShouldThrow()
        {
            //assert
            Assert.Throws<ArgumentNullException>(() => _sut.SubmitApplicationFor(null));
        }

        [Fact]
        public void ProductApplicationMediatorService_ForNullProduct_ShouldThrow()
        {
            //assert
            Assert.Throws<InvalidApplicationException>(() => _sut.SubmitApplicationFor(_sellerApplication.Object));
        }

        [Fact]
        public void ProductApplicationMediatorService_ForMultipleMatchingHandlers_ShouldThrow()
        {
            //arrange
            var businessLoansHandler1 = new Mock<IProductApplicationHandler>();
            var businessLoansHandler2 = new Mock<IProductApplicationHandler>();

            businessLoansHandler1.Setup(m => m.CanHandle(It.IsAny<BusinessLoans>())).Returns(true);
            businessLoansHandler2.Setup(m => m.CanHandle(It.IsAny<BusinessLoans>())).Returns(true);

            var sellerApplication = new Mock<ISellerApplication>();
            sellerApplication.SetupProperty(p => p.CompanyData, new SellerCompanyData());
            sellerApplication.SetupProperty(p => p.Product, new BusinessLoans());

            var sut = new ProductApplicationMediatorService(new List<IProductApplicationHandler> { businessLoansHandler1.Object, businessLoansHandler2.Object });

            //assert
            Assert.Throws<InvalidApplicationException>(() => _sut.SubmitApplicationFor(_sellerApplication.Object));
        }

        [Fact]
        public void ProductApplicationMediatorService_NullConstructorParameters_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => new ProductApplicationMediatorService(null));
        }

        private class ProductWithoutHandler : IProduct
        {
            public int Id { get; set; }
        }
    }
}