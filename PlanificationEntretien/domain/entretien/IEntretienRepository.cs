using System.Collections.Generic;
using PlanificationEntretien.domain.candidat;

namespace PlanificationEntretien.domain.entretien;

public interface IEntretienRepository
{
    Entretien FindById(int id);
    Entretien FindByCandidat(string candidatEmail);
    int Save(Entretien entretien);
    IEnumerable<Entretien> FindAll();
}