﻿using Autofac;
using NSubstitute;
using NUnit.Framework;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using System;
using System.Linq;
using System.Collections.Generic;
using SlothEnterprise.ProductApplication.Workflow;

namespace SlothEnterprise.ProductApplication.Tests
{
    [TestFixture]
    public class ProductApplicationTests
    {

        [OneTimeSetUp]
        public void SetupTests()
        {
            StartUp.Start();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            StartUp.Finish();
        }

        private static IReadOnlyDictionary<Type, Type> ProductServiceTypesMap { get; }
            = new Dictionary<Type, Type>()
        {
            { typeof(SelectiveInvoiceDiscount), typeof(ISelectInvoiceService) },
            { typeof(ConfidentialInvoiceDiscount), typeof(IConfidentialInvoiceService) },
            { typeof(BusinessLoans), typeof(IBusinessLoansService) }
        };

        public static IEnumerable<TestCaseData> ApplicationsAndServices { get; } =
            ProductServiceTypesMap.Select(
                (x, idx) => new TestCaseData(
                    new SellerApplication
                    {
                        CompanyData = Substitute.For<ISellerCompanyData>(),
                        Product = (IProduct)Substitute.For(new[] { x.Key }, null)
                    },
                    x.Value
                )
            );
        
        [TestCaseSource(nameof(ApplicationsAndServices))]
        public void ApplyToProductGetSuccess(ISellerApplication application, Type sutServiceType)
        {
            // arrange
            var givenApplicationResultId = 1;
            var selectInvoiceService       = StartUp.Scope.Resolve<ISelectInvoiceService>();
            var confidentialInvoiceService = StartUp.Scope.Resolve<IConfidentialInvoiceService>();
            var businessLoansService       = StartUp.Scope.Resolve<IBusinessLoansService>();

            var routeException = new Exception("Product routed to wrong service");
            var givenApplicationResult = new ApplicationResult(givenApplicationResultId);

            selectInvoiceService.SubmitApplicationFor(Arg.Any<string>(), Arg.Any<decimal>(), Arg.Any<decimal>())
                .Returns(x => sutServiceType == typeof(ISelectInvoiceService) ? givenApplicationResultId : throw routeException);
            confidentialInvoiceService.SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), Arg.Any<decimal>(), Arg.Any<decimal>(), Arg.Any<decimal>())
                .Returns(x => sutServiceType == typeof(IConfidentialInvoiceService) ? givenApplicationResult : throw routeException);
            businessLoansService.SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), Arg.Any<LoansRequest>())
                .Returns(x => sutServiceType == typeof(IBusinessLoansService) ? givenApplicationResult : throw routeException);

            var productServiceReceiveMap = new Dictionary<Type, Action>
            {
                { typeof(SelectiveInvoiceDiscount), () => selectInvoiceService.ReceivedWithAnyArgs().SubmitApplicationFor("", 0, 0) },
                { typeof(ConfidentialInvoiceDiscount), () => confidentialInvoiceService.ReceivedWithAnyArgs().SubmitApplicationFor(null, 0, 0, 0) },
                { typeof(BusinessLoans), () => businessLoansService.ReceivedWithAnyArgs().SubmitApplicationFor(null , null) },
            };

            var parameter = new PositionalParameter(0, null);
            parameter = new PositionalParameter(0, StartUp.Scope.Resolve<IProductResolver>(parameter));
            var productService = StartUp.Scope.Resolve<IProductApplicationService>(parameter);

            //act
            var applicationResult = productService.SubmitApplicationFor(application);

            //assert            
            var receiveHandler = productServiceReceiveMap.FirstOrDefault(x => x.Key.IsAssignableFrom(application.Product.GetType())).Value;
            Assert.IsNotNull(receiveHandler, "Substitute receiveHandler is not assigned to a product");
            receiveHandler();
            Assert.AreEqual(givenApplicationResultId, applicationResult.ApplicationId);
        }
    }
}
