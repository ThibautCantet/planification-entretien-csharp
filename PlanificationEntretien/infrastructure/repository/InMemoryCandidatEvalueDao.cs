using Candidat.domain;
using PlanificationEntretien.domain.entretien;

namespace PlanificationEntretien.infrastructure.repository;

public class InMemoryCandidatEvalueDao : ICandidatEvalueDAO
{
    private readonly ICandidatRepository _inMemoryCandidatRepository;

    public InMemoryCandidatEvalueDao(ICandidatRepository inMemoryCandidatRepository)
    {
        _inMemoryCandidatRepository = inMemoryCandidatRepository;
    }

    public CandidatEvalué FindById(int candidatEvalueId)
    {
        var candidat = _inMemoryCandidatRepository.FindById(candidatEvalueId);
        return new CandidatEvalué(candidat.Id, candidat.Language, candidat.Email, candidat.ExperienceEnAnnees);
    }
}