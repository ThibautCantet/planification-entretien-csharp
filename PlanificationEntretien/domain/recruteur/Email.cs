using System;
using System.Net.Mail;

namespace PlanificationEntretien.domain.recruteur;

public class Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (!IsValid(value) || !value.EndsWith("soat.fr"))
        {
            throw new ArgumentException($"{value} is not a valid email");
        }
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