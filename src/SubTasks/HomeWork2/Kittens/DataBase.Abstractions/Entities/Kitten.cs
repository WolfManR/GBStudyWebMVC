using System.Collections.Generic;

namespace DataBase.Abstractions.Entities
{
    public class Kitten : Patient
    {
        public string Nickname { get; set; }
        public double Weight { get; set; }
        public string Color { get; set; }
        public bool HasCertificate { get; set; }
        public string Feed { get; set; }
    }
}
