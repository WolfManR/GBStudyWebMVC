namespace BusinessLayer.Abstractions.Validations
{
    public class OperationFailure : IOperationFailure
    {
        public string PropertyName { get; init; }
        public string Description { get; init; }
        public string Code { get; init; }
    }
}