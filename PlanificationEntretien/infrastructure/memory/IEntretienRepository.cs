using System.Collections.Generic;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.infrastructure.memory;

public interface IEntretienRepository
{
    Entretien FindByCandidat(Candidat candidat);
    void Save(Entretien entretien);
    IEnumerable<Entretien> FindAll();
}