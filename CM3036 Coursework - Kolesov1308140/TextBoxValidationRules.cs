using System;
using System.Globalization;
using System.Windows.Controls;

namespace CM3036_Coursework___Kolesov1308140
{
    class TextBoxValidationRules : ValidationRule
    {
        public string TextBoxRuleType { get; set; }
        public bool IsNonSubmission { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
           var input = value as string;
            switch (TextBoxRuleType)
            {
                case "MatriculationNumber":
                    if(input == null) return new ValidationResult(false, "Please enter between 1 and 20 numbers");
                    break;
                case "FirstName":
                    if (input == null || input.Length < 2) return new ValidationResult(false, "Please enter between 2 and 50 letters");
                    break;
                case "LastName":
                    if (input == null || input.Length < 2) return new ValidationResult(false, "Please enter between 2 and 50 letters");
                    break;
                case "ComponentOne":
                    if ((input == null || input.Length != 3) && !IsNonSubmission) return new ValidationResult(false, "Please enter 3 Grades (A, B, C, D, E, F) exactly.");
                    break;
                case "ComponentTwo":
                    if ((input == null || input.Length != 5) && !IsNonSubmission) return new ValidationResult(false, "Please enter 5 Grades (A, B, C, D, E, F) exactly.");
                    break;
                case "ComponentThree":
                    if ((input == null || input.Length != 2) && !IsNonSubmission) return new ValidationResult(false, "Please enter 2 Grades (A, B, C, D, E, F) exactly.");
                    break;
            }

            
            return new ValidationResult(true, null);

        }
    }
}
