using System.Collections.Generic;

namespace PlanificationEntretien.domain;

public interface IEntretienPort
{
    Entretien FindByCandidat(Candidat candidat);
    void Save(Entretien entretien);
    IEnumerable<Entretien> FindAll();
}