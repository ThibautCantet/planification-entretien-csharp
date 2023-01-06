using System;
using PlanificationEntretien.domain.candidat;

namespace PlanificationEntretien.use_case;

public class CreerCandidat
{
    private readonly ICandidatRepository _candidatRepository;

    public CreerCandidat(ICandidatRepository candidatRepository)
    {
        _candidatRepository = candidatRepository;
    }

    public int Execute(String language, String email, int? experienceEnAnnees)
    {
        try
        {
            var candidat = new Candidat(language,
                email,
                experienceEnAnnees);
            return _candidatRepository.Save(candidat);
        }
        catch (ArgumentException)
        {
            return -1;
        }
    }
}