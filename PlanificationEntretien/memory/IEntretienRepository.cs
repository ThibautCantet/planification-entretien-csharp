using System.Collections.Generic;
using PlanificationEntretien.model;

namespace PlanificationEntretien.memory;

public interface IEntretienRepository
{
    Entretien FindByCandidat(Candidat candidat);
    void Save(Entretien entretien);
    IEnumerable<Entretien> FindAll();
}