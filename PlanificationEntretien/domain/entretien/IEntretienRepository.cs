using System.Collections.Generic;

namespace PlanificationEntretien.domain.entretien;

public interface IEntretienRepository
{
    Entretien FindById(int id);
    Entretien FindByCandidat(CandidatEvalué candidatEvalué);
    int Save(Entretien entretien);
    IEnumerable<Entretien> FindAll();
}