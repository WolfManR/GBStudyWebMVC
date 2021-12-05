namespace BusinessLayer.Abstractions.Models
{
    public class Clinic : IEntity<int>
    {
        public int Id { get; init; }
        public string Name { get; set; }
    }
}