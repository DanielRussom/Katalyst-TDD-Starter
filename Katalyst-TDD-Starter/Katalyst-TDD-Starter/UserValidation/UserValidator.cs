﻿namespace Katalyst_TDD_Starter.UserValidation
{
    public class UserValidator : IUserValidator
    {
        public ValidationResult Validate(User userToValidate)
        {
            if (userToValidate.FirstOrLastNameIsEmpty())
            {
                return ValidationResult.Invalid;
            }
            
            if (!userToValidate.HasValidEmail())
            {
                return ValidationResult.Invalid;
            }

            return ValidationResult.Valid;
        }
    }
}