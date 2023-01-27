using System.Collections.Generic;

namespace PlanificationEntretien.domain;

public interface IEntretienRepository
{
    Entretien FindById(int id);
    Entretien FindByCandidat(Candidat candidat);
    int Save(Entretien entretien);
    IEnumerable<Entretien> FindAll();
}