using System;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.use_case;

public class CreerCandidat
{
    private readonly ICandidatRepository _candidatRepository;

    public CreerCandidat(ICandidatRepository candidatRepository)
    {
        _candidatRepository = candidatRepository;
    }

    public bool Execute(String language, String email, int? experienceEnAnnees)
    {
        try
        {
            var candidat = new Candidat(language,
                email,
                experienceEnAnnees);
            _candidatRepository.Save(candidat);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}