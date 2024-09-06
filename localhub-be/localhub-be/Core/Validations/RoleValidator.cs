using System.ComponentModel.DataAnnotations;

namespace localhub_be.Core.Validations;
public class RoleValidator {
    public static ValidationResult ValidateRole(string role, ValidationContext validationContext) {
        if (!role.Equals("User") || !role.Equals("Administrator")) {
            return new ValidationResult("The user's role in the application must be either 'User' or 'Administrator'.");
        }

        return ValidationResult.Success;
    }
}
