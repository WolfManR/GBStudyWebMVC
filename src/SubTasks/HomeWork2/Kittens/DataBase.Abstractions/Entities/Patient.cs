using System.Collections.Generic;
using DataBase.Abstractions.Entities.Analyzes;

namespace DataBase.Abstractions.Entities
{
    public abstract class Patient : IEntity<int>
    {
        public int Id { get; init; }
        public ICollection<Analysis> Analysis { get; init; }
        public ICollection<Clinic> Clinics { get; init; }
    }
}