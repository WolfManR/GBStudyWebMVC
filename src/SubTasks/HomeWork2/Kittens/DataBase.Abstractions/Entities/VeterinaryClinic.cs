namespace DataBase.Abstractions.Entities
{
    public class VeterinaryClinic : IEntity<int>
    {
        public int Id { get; init; }

        public string Name { get; set; }
        public string[] HealsAnimals { get; set; }
    }
}