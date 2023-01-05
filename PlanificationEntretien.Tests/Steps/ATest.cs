using PlanificationEntretien.domain;
using PlanificationEntretien.infrastructure.repository;

namespace PlanificationEntretien.Steps;

public abstract class ATest
{
    protected IEntretienRepository EntretienRepository = new InMemoryEntretienRepository();
    protected IRecruteurRepository RecruteurRepository = new InMemoryRecruteurRepository();
    protected ICandidatRepository CandidatRepository = new InMemoryCandidatRepository();
}