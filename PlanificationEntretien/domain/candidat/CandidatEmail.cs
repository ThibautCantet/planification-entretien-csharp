using System;
using System.Net.Mail;

namespace PlanificationEntretien.domain.candidat;

record CandidatEmail() {
    private static readonly string EMAIL_REGEX = "^[\\w-_.+]*[\\w-_.]@([\\w]+\\.)+[\\w]+[\\w]$";

    public CandidatEmail(string adresse) : this() {
        if (!IsValid(adresse) || adresse.EndsWith("soat.fr")) {
            throw new ArgumentException();
        }
        Adresse = adresse;
    }

    public string Adresse { get; }

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
}