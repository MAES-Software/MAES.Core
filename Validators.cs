using System.ComponentModel.DataAnnotations;

namespace MAES.Core;

public class OIBValidator : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (validationContext.MemberName == null) throw new ArgumentNullException("validationContext.MemberName");
        if (string.IsNullOrEmpty((string?)value)) return new ValidationResult("OIB mora biti ispunjen", [validationContext.MemberName]);

        string oib = (string)value!;

        if (oib.StartsWith("strani:")) return null;

        if (oib.Length != 11 || !oib.All(char.IsDigit)) return new ValidationResult("OIB se mora sastojati od 11 brojeva", [validationContext.MemberName]);

        int a = 10;

        for (int i = 0; i < 10; i++)
        {
            a = (a + int.Parse(oib.Substring(i, 1))) % 10;
            if (a == 0) a = 10;
            a = a * 2 % 11;
        }

        if (int.Parse(oib.Substring(10, 1)) != (11 - a) % 10) return new ValidationResult("OIB neispravan", [validationContext.MemberName]);

        return null;
    }
}

public class MustBeTrueAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is bool boolean && boolean;
    }
}