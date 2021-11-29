namespace PaymentsDomain.SeedWork
{
    /// <summary>
    /// Custom entity base class please for more details refer to https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/seedwork-domain-model-base-classes-interfaces.
    /// Given the fact that this implimentation is limited and not all methods are used, the calss has been simplified. 
    /// For example domain events are not supported.
    /// </summary>
    public abstract class Entity
    {
        public virtual int Id { get; protected set; }
    }
}
