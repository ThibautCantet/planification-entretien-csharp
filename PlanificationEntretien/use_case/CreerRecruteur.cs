using System;
using System.Net.Mail;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.use_case;

public class CreerRecruteur
{
    private readonly IRecruteurPort _recruteurPort;

    public CreerRecruteur(IRecruteurPort recruteurPort)
    {
        _recruteurPort = recruteurPort;
    }

    public bool Execute(Recruteur recruteur)
    {
        if (!string.IsNullOrEmpty(recruteur.Email) && IsValid(recruteur.Email)
                                                   && !string.IsNullOrEmpty(recruteur.Language)
                                                   && recruteur.ExperienceEnAnnees > 0)
        {
            _recruteurPort.Save(recruteur);
            return true;
        }

        return false;
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
}