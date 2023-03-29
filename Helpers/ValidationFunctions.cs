using System.Data.Entity.ModelConfiguration;

namespace OrderManagementWebAPI.Helpers
{
    public class ValidationFunctions
    {
        public static void ExceptionWhenSizeNotInRange(float dimension)
        {
            if (dimension < 5 || dimension > 150)
            {
                throw new ModelValidationException(ErrorMessagesEnum.LabelSize);
            }
        }
    }
}
