# About The Project
This repo is exposing three endpoints. swagger was used for display and test porpuses. The url when running the project by default will be localhost/swagger/index.html
- Get - to get all payment requests, 
- GetById - to get a payment request,
- Post - to create a payment request the json for the create looks like 
```
{
  "amount": 0,
  "card": {
    "name": "string",
    "cardNumber": "string",
    "expiryDate": "string",
    "cvv": "string"
  },
  "billingAddress": {
    "line1": "string",
    "line2": "string",
    "line3": "string",
    "postCode": "string"
  }
}
```

## Technologies that were used
- .Net core 3.1
- Entity Framework core 3.1 (SqlLite was used to store the data in memory instead of a database)

## Patterns that were followed
- DDD
- TDD (with xUnit)
- CQRS (MediatR was used to impliment it)
- UnitOfWork

## Libraries that were used
- MediatR
- AutoMapper
- FluentValidation
- xUnit
- Moq
