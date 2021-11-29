using PaymentsDomain.SeedWork;

namespace PaymentsDomain.AggregatesModel.PaymentAggregate
{
    /// <summary>
    /// Payment: is the main entry point for all entities.
    /// In terms of DDD is called aggregate root.
    /// All setters are defined as private in order to achive immutability. 
    /// All properties should never be modified outside of the calss. Instead use methods to modify them.
    /// </summary>
    public class Payment : Entity, IAggregateRoot
    {
        #region Columns

        public decimal Amount { get; private set; }

        #endregion

        #region Navigation properties

        public Card Card { get; private set; }

        
        //Here I made the assumption the the billing address can be different than the addres that the card was registered on.
        //If that assumption is wrong then the billing address needs to be in the Card.
        public BillingAddress BillingAddress { get; private set; }

        #endregion

        #region Constructors

        public Payment()
        {

        }

        public Payment(decimal amount, Card card, BillingAddress billingAddress) : this()
        {
            Amount = amount;

            UpdateCard(card);
            UpdateBillingAddress(billingAddress);
        }


        #endregion

        #region Methods 

        /// <summary>
        /// Update card
        /// </summary>
        /// <param name="card"></param>
        public void UpdateCard(Card card)
        {
            //Allready up to date
            if (this.Card != null && this.Card.Equals(card))
                return;

            //Update card
            this.Card = card;
            
        }

        /// <summary>
        /// Update billing address
        /// </summary>
        /// <param name="billingAddress"></param>
        public void UpdateBillingAddress(BillingAddress billingAddress)
        {
            //Allready up to date
            if (this.BillingAddress != null && this.BillingAddress.Equals(billingAddress))
                return;

            //Update billing address
            this.BillingAddress = billingAddress;
        }

        #endregion


    }
}
