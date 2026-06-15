using PlanificationEntretien.Candidat.Domain;
using PlanificationEntretien.Candidat.Infrastructure.Repository;
using PlanificationEntretien.Entretien.Domain;
using PlanificationEntretien.entretien.Infrastructure.Repository;
using PlanificationEntretien.Recruteur.Domain;
using PlanificationEntretien.Recruteur.Infrastructure.Repository;

namespace PlanificationEntretien.Steps;

public abstract class ATest
{
    protected IEntretienRepository EntretienRepository = new InMemoryEntretienRepository();
    protected IRecruteurRepository RecruteurRepository = new InMemoryRecruteurRepository();
    protected ICandidatRepository CandidatRepository = new InMemoryCandidatRepository();
}