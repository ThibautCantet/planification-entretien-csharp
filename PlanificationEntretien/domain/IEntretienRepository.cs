using System.Collections.Generic;

namespace PlanificationEntretien.domain;

public interface IEntretienRepository
{
    Entretien FindByCandidat(Candidat candidat);
    int Save(Entretien entretien);
    IEnumerable<Entretien> FindAll();
}