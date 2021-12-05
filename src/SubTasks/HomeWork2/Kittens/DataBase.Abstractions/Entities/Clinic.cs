using System.Collections.Generic;
using DataBase.Abstractions.Entities.Analyzes;

namespace DataBase.Abstractions.Entities
{
    public class Clinic : IEntity<int>
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public ICollection<Patient> Patients { get; init; }
        public ICollection<Analysis> Analyzes { get; init; }
    }
}