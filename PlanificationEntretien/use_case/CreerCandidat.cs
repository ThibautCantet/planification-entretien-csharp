using System;
using System.Net.Mail;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.use_case;

public class CreerCandidat
{
    private readonly ICandidatRepository _candidatRepository;

    public CreerCandidat(ICandidatRepository candidatRepository)
    {
        _candidatRepository = candidatRepository;
    }

    public bool Execute(Candidat candidat)
    {
        if (!string.IsNullOrEmpty(candidat.Email) && IsValid(candidat.Email)
                                                  && !string.IsNullOrEmpty(candidat.Language)
                                                  && candidat.ExperienceEnAnnees > 0)
        {
            _candidatRepository.Save(candidat);
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