using System.ComponentModel.DataAnnotations;

namespace QRGenerator.Persentation.Web.CustomValidation;
public class MustContainUppercaseAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is string str)
        {
            return str.All(char.IsUpper);
        }
        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} must be uppercase.";
    }
}