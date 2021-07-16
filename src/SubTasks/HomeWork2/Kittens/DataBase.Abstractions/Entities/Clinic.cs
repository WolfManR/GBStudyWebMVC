using System.Collections.Generic;

namespace DataBase.Abstractions.Entities
{
    public class Clinic : IEntity<int>
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public ICollection<Kitten> Kittens { get; init; }
    }
}