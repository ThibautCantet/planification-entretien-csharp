using System;
using System.Net.Mail;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.use_case;

public class CreerCandidat
{
    private readonly ICandidatPort _candidatPort;

    public CreerCandidat(ICandidatPort candidatPort)
    {
        _candidatPort = candidatPort;
    }

    public bool Execute(Candidat candidat)
    {
        if (!string.IsNullOrEmpty(candidat.Email) && IsValid(candidat.Email)
                                                  && !string.IsNullOrEmpty(candidat.Language)
                                                  && candidat.ExperienceEnAnnees > 0)
        {
            _candidatPort.Save(candidat);
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