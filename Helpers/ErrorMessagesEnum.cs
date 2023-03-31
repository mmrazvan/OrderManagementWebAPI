namespace OrderManagementWebAPI.Helpers
{
    public class ErrorMessagesEnum
    {
        public const string NoElementFound = "No element found in table!";
        public const string LabelSize = "Labes size must be between 5 and 150";
        public const string BadRequest = "Body is not correct!";
        public const string FieldRequied = "requied!";
        public const string OrderInProduction = "Cannot modify/delete an order in production";
        public const string DatabaseNotEmpty = "Cannot add data because there is old data there. Delete old data first.";
        public const string OrderLabelsMissing = "There are no order labels. Please create order labels.";
    }
}
