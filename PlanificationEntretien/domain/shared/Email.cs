using System;
using System.Net.Mail;

namespace PlanificationEntretien.domain.shared;

public class Email
{
    public string Value { get; }

    public static Email EmailRecruteur(string value)
    {
        if (!IsValid(value) || !value.EndsWith("soat.fr"))
        {
            throw new ArgumentException($"{value} is not a valid email");
        }
        return new Email(value);
    }
    
    public static Email EmailCandidat(string value)
    {
        if (!IsValid(value) || value.EndsWith("soat.fr"))
        {
            throw new ArgumentException($"{value} is not a valid email");
        }
        return new Email(value);
    }
    
    private Email(string value)
    {
        Value = value;
    }

    private static bool IsValid(string email)
    {
        try
        {
            new MailAddress(email);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

};