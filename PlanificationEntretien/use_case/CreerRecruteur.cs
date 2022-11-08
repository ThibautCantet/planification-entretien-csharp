using System;
using System.Net.Mail;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.use_case;

public class CreerRecruteur
{
    private readonly IRecruteurRepository _recruteurRepository;

    public CreerRecruteur(IRecruteurRepository recruteurRepository)
    {
        _recruteurRepository = recruteurRepository;
    }

    public bool Execute(Recruteur recruteur)
    {
        if (!string.IsNullOrEmpty(recruteur.Email) && IsValid(recruteur.Email)
                                                   && !string.IsNullOrEmpty(recruteur.Language)
                                                   && recruteur.ExperienceEnAnnees > 0)
        {
            _recruteurRepository.Save(recruteur);
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