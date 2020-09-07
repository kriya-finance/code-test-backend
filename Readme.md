# MarketFinance C# Backend Coding Exercise
At Sloth Enterprise, we offer our customers 3 different products.
* Select Invoice Discount
* Confidential Invoice Discount
* Business Loans

Each product has different application criteria and is hosted in its very own microservice. Sloth Enterprise provides a single service : `ProductApplicationService` as a nexus for these 3 microservices. The said service is hosted in this project.

---

This project consists of a single service that receives a single customer application for a specific service, then routes it to the respective product microservice.

This code was written by a baby sloth some time ago and needs a bit of TLC. 


## The scope of the exercise is as follows:

1. Describe in `Comments.md` the code issues with `ProductApplicationService`. List top 5 main areas of concern.
2. Refactor `ProductApplicationService`. (do not touch SlothEnterprise.External)

We advise you to take between 60 to 90 minutes to complete this task.

Note that we have added xUnit as the default testing framework but you are welcome to use whatever testing framework you are most comfortable with

### What we pay attention to when we evaluate your submission
* Code quality and consistency
* Readability and simplicity
* Backwards compatibility
* Test quality
* Commit history


## Submission Guidelines
Here are guidelines on how to submit the exercise

### If you are in contact with our Talent Team
* ZIP your code directory, *without* uncommitted / ignored files _(i.e. NuGet packages)_
  - include git, but exclude anything not checked in _(git clean -fdx)_.
* Email the submission in a zip file to our Talent team contact

### Getting in contact with our talent team
Get in touch with us via email at [recruitment@marketfinance.com](recruitment@marketfinance.com)!

#### Thank you for taking your time
[MarketFinance Tech Team](https://github.com/marketfinance)
