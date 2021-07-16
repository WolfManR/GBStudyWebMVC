namespace DataLayer.Abstractions.Entities
{
    public class Clinic : IEntity<int>
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }
}