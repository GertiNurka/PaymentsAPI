using System.Collections.Generic;
using PaymentsDomain.SeedWork;

namespace PaymentsDomain.AggregatesModel.PaymentAggregate
{
    /// <summary>
    /// Card is value object of payment (for value objects pelase refer to https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects)
    /// All setters are defined as private in order to achive immutability. 
    /// All properties should never be modified outside of the calss. Instead use methods to modify them.
    /// </summary>
    public class Card : ValueObject
    {
        #region Columns

        public string Name { get; private set; }
        public string CardNumber { get; private set; }
        public string ExpiryDate { get; private set; }
        public string Cvv { get; private set; }

        #endregion

        #region Constructors

        public Card() { }
        public Card(string name, string cardNumber, string expiryDate, string cvv) : this()
        {
            Name = name;
            CardNumber = cardNumber;
            ExpiryDate = expiryDate;
            Cvv = cvv;
        }

        #endregion

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Name;
            yield return CardNumber;
            yield return ExpiryDate;
            yield return Cvv;
        }
    }
}