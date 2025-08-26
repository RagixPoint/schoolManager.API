using System;

namespace SchoolManager.Application.Common.Exceptions;

public class AccountInactiveException : Exception
{
    public AccountInactiveException() : base("credentials.error.inactive")
    {
    }
    
    public AccountInactiveException(string message) : base(message)
    {
    }
}