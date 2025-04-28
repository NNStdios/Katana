namespace ArtificeToolkit.Attributes
{
    public class ValidateInputAttribute : ValidatorAttribute
    {
        public string Condition;
        public string Message = "";

        public ValidateInputAttribute(string condition)
        {
            Condition = condition;
        }

        public ValidateInputAttribute(string condition, string message)
        {
            Condition = condition;
            Message   = message;
        }
    }
}