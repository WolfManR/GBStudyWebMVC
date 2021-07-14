namespace DataBase.Abstractions.Entities
{
    public class Kitten : IEntity<int>
    {
        public int Id { get; init; }
        public string Nickname { get; set; }
        public double Weight { get; set; }
        public string Color { get; set; }
        public bool HasCertificate { get; set; }
        public string Feed { get; set; }
    }
}
