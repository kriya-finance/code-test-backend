Main concerns to refactor solution
#1 Project has strongly coupled product-to-service workflow, it will not allow modifying/adding that structure without many efforts and testing
#2 Solution lacks many important things - rational tests, which will cover various potential changes and keep working logic secured
#3 Quite naive implementation with big methods, repititive code, poor encapsulation, weak and unclear Exception handling, quite a good UE could be added via asynchonous interaction
#4 With some techniques like dependency injection project could be pulled to a much more maintainable scheme, which allows easily add/modify products and services
#5 Instead of hard-coding approach there quite easy and strong methods in the field of data-driven-development, where lots done via configurative/declarative manner, pursuing SSoT