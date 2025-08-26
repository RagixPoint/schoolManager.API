using System;

namespace SchoolManager.Application.Common.Exceptions;

public class EmailAlreadyExistsException : Exception
{
    public EmailAlreadyExistsException() : base("email.already.exists")
    {
    }

    public EmailAlreadyExistsException(string message) : base(message)
    {
    }
}