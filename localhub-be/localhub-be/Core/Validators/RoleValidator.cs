using System.ComponentModel.DataAnnotations;

namespace localhub_be.Core.Validators;
public class RoleValidator {
    public static ValidationResult ValidateRole(string role, ValidationContext validationContext) {
        if (role.Equals("User") || role.Equals("Administrator")) {
            return ValidationResult.Success;
        }

        return new ValidationResult("The user's role in the application must be either 'User' or 'Administrator'.");
    }
}
