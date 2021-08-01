namespace DataBase.Abstractions.Entities.Analyzes
{
    public abstract class Analysis : IEntity<int>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public bool Paid { get; init; }
        public string Result { get; init; }
        public Patient Patient { get; init; }
        public Clinic Clinic { get; init; }
    }
}