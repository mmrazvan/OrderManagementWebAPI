
using OrderManagementWebAPI.Model;

namespace OrderManagementWebAPI.Helpers
{
    public static class ValidationFunctions
    {
        public static void ExceptionWhenSizeNotInRange(float dimension)
        {
            if (dimension < 5 || dimension > 150)
            {
                throw new ModelValidationException(ErrorMessagesEnum.LabelSize);
            }
        }

        public static void ExceptionWhenFieldRequied(string  field)
        {
            if (string.IsNullOrEmpty(field))
            {
                throw new ModelValidationException($"The field {field} is {ErrorMessagesEnum.LabelSize}"); 
            }
        }
    }
}
