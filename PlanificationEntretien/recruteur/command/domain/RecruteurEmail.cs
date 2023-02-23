using System;
using System.Net.Mail;

namespace PlanificationEntretien.recruteur.domain;

record RecruteurEmail() {
    private static readonly string EMAIL_REGEX = "^[\\w-_rendreRecruteurIndisponible.+]*[\\w-_rendreRecruteurIndisponible.]@([\\w]+\\.)+[\\w]+[\\w]$";

    public RecruteurEmail(string adresse) : this() {
        if (!IsValid(adresse) || !adresse.EndsWith("soat.fr")) {
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