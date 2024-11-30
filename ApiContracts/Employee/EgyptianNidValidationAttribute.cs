using System.ComponentModel.DataAnnotations;
using System.Globalization;
namespace ApiContracts.Employee;
public class EgyptianNidValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("National ID is required.");
        }

        var nid = value.ToString();

        if (nid!.Length != 14)
        {
            return new ValidationResult("National ID must be exactly 14 digits.");
        }

        if (!long.TryParse(nid, out _))
        {
            return new ValidationResult("National ID must contain only numeric characters.");
        }

        int century = int.Parse(nid[0].ToString());
        if (century != 2 && century != 3)
        {
            return new ValidationResult("National ID must start with 2 or 3 (valid centuries).");
        }

        string dobPart = nid.Substring(1, 6);
        if (!DateTime.TryParseExact(dobPart, "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
        {
            return new ValidationResult("Invalid date of birth in National ID.");
        }

        string governorateCode = nid.Substring(7, 2);
        if (!IsValidGovernorateCode(governorateCode))
        {
            return new ValidationResult("Invalid governorate code in National ID.");
        }

        if (!IsValidChecksum(nid))
        {
            return new ValidationResult("Invalid checksum in National ID.");
        }

        return ValidationResult.Success!;
    }

    private bool IsValidGovernorateCode(string code)
    {
        var validCodes = new[]
        {
            "01", "02", "03", "04", "11", "12", "13", "14", "15", "16", "17", "18", "19",
            "21", "22", "23", "24", "25", "26", "27", "28", "29", "31", "32", "33", "34", "35"
        };
        return Array.Exists(validCodes, c => c == code);
    }

    private bool IsValidChecksum(string nid)
    {
        int[] digits = nid.Select(c => int.Parse(c.ToString())).ToArray();

        int sum = 0;

        for (int i = 0; i < digits.Length - 1; i++)
        {
            int digit = digits[i];

            if (i % 2 == 0)
            {
                digit *= 2;
                if (digit > 9) digit -= 9;
            }

            sum += digit;
        }

        int checksumDigit = digits[^1];

        return (sum % 10) == checksumDigit;
    }
}
