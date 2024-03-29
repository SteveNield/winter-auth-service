﻿namespace Winter.Auth.Service.Models
{
    public class LoginAttemptValidator : IDtoValidator<LoginAttempt>
    {
        public bool IsValid(LoginAttempt dtoToValidate)
        {
            return !string.IsNullOrWhiteSpace(dtoToValidate.Username) && !string.IsNullOrWhiteSpace(dtoToValidate.Password);
        }
    }
}