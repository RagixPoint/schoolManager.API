using System;

namespace SchoolManager.Application.Common.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException() : base("credentials.error")
    {
    }

    public InvalidCredentialsException(string message) : base(message)
    {
    }
}