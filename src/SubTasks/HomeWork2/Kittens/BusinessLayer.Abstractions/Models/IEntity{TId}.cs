namespace BusinessLayer.Abstractions.Models
{
    public interface IEntity<TId>
    {
        public TId Id { get; init; }
    }
}