// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.\r
// SPDX-License-Identifier: Apache-2.0

using System.Text.RegularExpressions;

namespace AWS.Deploy.Common.Recipes.Validation
{
    /// <summary>
    /// The validator is typically used with OptionSettingItems which have a string type.
    /// The regex string is specified in the deployment recipe
    /// and this validator checks if the set value of the OptionSettingItem matches the regex or not.
    /// </summary>
    public class RegexValidator : IOptionSettingItemValidator
    {
        private static readonly string defaultRegex = "(.*)";
        private static readonly string defaultValidationFailedMessage = "Value must match Regex {{Regex}}";

        public string Regex { get; set; } = defaultRegex;
        public string ValidationFailedMessage { get; set; } = defaultValidationFailedMessage;
        public bool AllowEmptyString { get; set; }

        public ValidationResult Validate(object input)
        {
            var regex = new Regex(Regex);

            var message = ValidationFailedMessage.Replace("{{Regex}}", Regex);

            return new ValidationResult
            {
                IsValid = regex.IsMatch(input?.ToString() ?? "") || (AllowEmptyString && string.IsNullOrEmpty(input?.ToString())),
                ValidationFailedMessage = message
            };
        }
    }
}