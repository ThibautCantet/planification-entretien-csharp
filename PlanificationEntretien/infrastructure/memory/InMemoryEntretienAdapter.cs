using System.Collections.Generic;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.infrastructure.memory;

public class InMemoryEntretienAdapter : IEntretienPort
{
    private Dictionary<Candidat, Entretien> _entretiens = new();

    public Entretien FindByCandidat(Candidat candidat)
    {
        Entretien value;
        _entretiens.TryGetValue(candidat, out value);
        return value;
    }

    public void Save(Entretien entretien)
    {
        _entretiens.Add(entretien.Candidat, entretien);
    }

    public IEnumerable<Entretien> FindAll()
    {
        return _entretiens.Values;
    }
}