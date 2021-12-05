namespace DataLayer.Abstractions.Entities
{
    public interface IEntity<TId>
    {
        public TId Id { get; init; }
    }
}