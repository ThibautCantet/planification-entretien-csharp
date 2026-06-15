using PlanificationEntretien.Candidat.Domain;
using PlanificationEntretien.Candidat.Infrastructure.Repository;
using PlanificationEntretien.Entretien.Domain;
using PlanificationEntretien.entretien.Infrastructure.Repository;
using PlanificationEntretien.Recruteur.Domain;
using PlanificationEntretien.Recruteur.Infrastructure.Repository;

namespace PlanificationEntretien.Steps;

public abstract class ATest
{
    protected const string ConnectionString =
        "Host=localhost;Port=5432;Database=planification_entretien;Username=postgres;Password=postgres";

    protected IEntretienRepository EntretienRepository = new PostgresEntretienRepository(ConnectionString);
    protected IRecruteurRepository RecruteurRepository = new PostgresRecruteurRepository(ConnectionString);
    protected ICandidatRepository CandidatRepository = new PostgresCandidatRepository(ConnectionString);
}