using System.Collections.Generic;

namespace PlanificationEntretien.Entretien.Domain;

public interface IEntretienRepository
{
    Entretien FindById(int id);
    Entretien FindByCandidat(string candidatEmail);
    int Save(Entretien entretien);
    IEnumerable<Entretien> FindAll();
}