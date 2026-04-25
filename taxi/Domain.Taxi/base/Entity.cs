namespace Domain.Taxi.Base
{
    public abstract class Entity<TId> where TId : struct
    {
        public TId Id { get; }

        protected Entity(TId id)
        {
            Id = id;
        }

        protected Entity() : this(default!)
        {
        }
    }
}