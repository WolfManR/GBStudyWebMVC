namespace KittensApi.Controllers.Responses
{
    public class KittenGetResponse
    {
        public int Id { get; init; }
        public string Nickname { get; init; }
        public double Weight { get; init; }
        public string Color { get; init; }
        public bool HasCertificate { get; init; }
        public string Feed { get; init; }
    }
}
