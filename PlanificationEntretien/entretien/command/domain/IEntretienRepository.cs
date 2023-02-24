
namespace PlanificationEntretien.entretien.domain
{
    public interface IEntretienRepository
    {
        Entretien FindById(int id);
        Entretien FindByCandidat(string candidatEmail);
        int Save(Entretien entretien);
    }
}