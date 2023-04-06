using System.Collections.Generic;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.entretien.infrastructure.repository;

namespace PlanificationEntretien.entretien.application_service.infrastructure;

public class InMemoryEntretienDao : IEntretienDao
{
    private readonly InMemoryEntretienRepository _repository;
    private readonly Dictionary<Candidat,InMemoryEntretien> _entretiens;

    public InMemoryEntretienDao(InMemoryEntretienRepository repository)
    {
        _entretiens = repository.Entretiens;
    }

    public IEnumerable<IEntretien> FindAll()
    {
        return _entretiens.Values;
    }
}