namespace PlanificationEntretien.domain
{
    public interface EntretienRepository
    {
        Entretien FindByCandidat(Candidat candidat);
    }
}