namespace PlanificationEntretien.domain
{
    public interface IEntretienRepository
    {
        Entretien FindByCandidat(Candidat candidat);
        void Save(Entretien entretien);
    }
}