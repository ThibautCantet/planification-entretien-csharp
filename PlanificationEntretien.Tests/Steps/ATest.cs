using PlanificationEntretien.candidat.domain;
using PlanificationEntretien.candidat.infrastructure.repository;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.entretien.infrastructure.repository;
using PlanificationEntretien.recruteur.domain;
using PlanificationEntretien.recruteur.infrastructure.repository;

namespace PlanificationEntretien.Steps;

public abstract class ATest
{
    protected IEntretienRepository EntretienRepository = new InMemoryEntretienRepository();
    protected IRecruteurRepository RecruteurRepository = new InMemoryRecruteurRepository();
    protected ICandidatRepository CandidatRepository = new InMemoryCandidatRepository();
}