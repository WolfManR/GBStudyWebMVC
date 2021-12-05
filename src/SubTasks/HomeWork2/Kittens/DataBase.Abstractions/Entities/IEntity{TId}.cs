namespace DataBase.Abstractions.Entities
{
    public interface IEntity<TId>
    {
        public TId Id { get; init; }
    }
}