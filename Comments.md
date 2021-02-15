1. It does not meet Open/Closed principle - that's the biggest propblem.
In case of new products we will have to each time modify ProductApplicationService - by adding new IF statement.

1.a. Extending this class with new product microservices also requires modification to unit tests - to keep tests still working.

2. ProductApplicationService contains many responsibilities:
- business logic to recognize which microservice should be called
- business logic of calling appropriate service
- mapping code that creates CompanyData

3. Potentially we may end up with Null Reference Exception when:
- accessing Product when application object is NULL
- building CompanyDataRequest (if application.CompanyData will be NULL)
Additionally I guess that we should not call microservice when CompanyData.Number = 0 (but it's related to business requirements).

4. When calling microservices we repeat code:
	-when building CompanyDataRequest
	-when returning integer based on IApplicationResult
Extension methods will help here.

5. Throwing just InvalidOperationException when no if statement for appropriate microservice matches, gives no information when we will investigate logs.
It would be good to add here custom error message or have own exception with more information why it failed.

There are other issues like:

* Returning just integer from the service does not bring much information.
It would be good to return IApplicationResult (unfortunately one of microservices already returns integer).
Thanks to that, when this service will be potentially used by some client (via WebAPI endpoint),
we can return appropriate status code and list of errors to client.

* Personally I wouldn't call microservice if CompanyNumber is 0 (it depends on business requirements)

* I prefer to have calculation of applicationId based on application result divided into if statements instead of one-liner - I did it in my solution

* Products contain a lot of properties with type decimal.
Maybe type 'double' will be enough but it depends on business requirements - so it's not a concern for this moment