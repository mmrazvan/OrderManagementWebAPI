namespace OrderManagementWebAPI.Model
{
    public class ModelValidationException : Exception
    {
        public ModelValidationException(string? message) : base(message) { }
    }
}
