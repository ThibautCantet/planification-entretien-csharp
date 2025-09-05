using Candidat.domain;
using Candidat.infrastructure.repository;
using PlanificationEntretien.domain.entretien;
using PlanificationEntretien.domain.recruteur;
using PlanificationEntretien.infrastructure.repository;

namespace PlanificationEntretien.Steps;

public abstract class ATest
{
    public ATest()
    {
        EntretienRepository = new InMemoryEntretienRepository();
        RecruteurRepository = new InMemoryRecruteurRepository();
        CandidatRepository = new InMemoryCandidatRepository();
        RecruteurAssigneDao = (IRecruteurAssigneDao)RecruteurRepository;
        CandidatEvalueDao = new InMemoryCandidatEvalueDao(CandidatRepository);
    }

    protected IEntretienRepository EntretienRepository;
    protected IRecruteurRepository RecruteurRepository;
    protected ICandidatRepository CandidatRepository;
    protected IRecruteurAssigneDao RecruteurAssigneDao;
    protected ICandidatEvalueDAO CandidatEvalueDao;
}