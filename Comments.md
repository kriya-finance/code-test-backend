Top concerns about exsting code:
- ProductApplicationService had multiple responsibilities. It was responsible for routing as well as excuting target service specific logic.
- ProductApplicationService was violating open closed principle. 
- Injecting external services in ProductApplicationService means a continuous growing list of external services injections. 
- Modifying ProductApplicationService could lead to bugs when added a new product type/external service type.
- Changes in other classes such as CompanyDataRequest or LoansRequest could lead to changes in ProductApplicationService
