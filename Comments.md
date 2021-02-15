1. Issues with SOLID principles

It does not meet Open/Closed principle - that's the biggest propblem.
In case of new products we will have to each time modify ProductApplicationService - by adding new IF statement

1.a. Extending this class with new product microservices also requires modification to unit tests - to keep tests still working
1.b. Additionally it does not meet Single responsibility principle - it contains business logic for both:
- recognize proper web service to call
- call to appropriate service

2. Potentially we may end up with Null Reference Exception when building CompanyDataRequest (if application.CompanyData will be null)
Additionally I guess that we should not call microservice when CompanyData.Number = 0 (but it's related to business requirements)

3. When calling microservices we repeat code:
	-when building CompanyDataRequest
	-when returning integer based on IApplicationResult
Extension methods will help here

4. Throwing just InvalidOperationException gives no information when we will investigate logs.
It would be good to add here custom error message or have own exception with more information why it failed

5. Returning just integer from the service does not bring much information. It would be good to return IApplicationResult.
Thanks to that, when this service will be potentially used by some client (via WebAPI endpoint),
we can return appropriate status code and list of errors to client

There are other issues like:

* Personally I wouldn't call microservice if CompanyNumber is 0 (it depends on business requirements)

* I prefer to have calculation of applicationId based on application result divided into if statements instead of one-liner

* Products contain a lot of properties with type decimal.
Maybe double will be enough but it depends on business requirements so it's not a concern for this moment