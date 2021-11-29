using System.Collections.Generic;
using PaymentsDomain.SeedWork;

namespace PaymentsDomain.AggregatesModel.PaymentAggregate
{
    /// <summary>
    /// Billing address is value object of payment (for value objects pelase refer to https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects)
    /// All setters are defined as private in order to achive immutability. 
    /// All properties should never be modified outside of the calss. Instead use methods to modify them.
    /// </summary>
    public class BillingAddress : ValueObject
    {
        #region Columns

        public string Line1 { get; private set; }
        public string Line2 { get; private set; }
        public string Line3 { get; private set; }
        public string PostCode { get; private set; }

        #endregion

        #region Constructors

        public BillingAddress()
        {

        }

        public BillingAddress(string line1, string line2, string line3, string poctCode)
        {
            Line1 = line1;
            Line2 = line2;
            Line3 = line3;
            PostCode = poctCode;
        }

        #endregion

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Line1;
            yield return Line2;
            yield return Line3;
            yield return PostCode;
        }
    }
}
