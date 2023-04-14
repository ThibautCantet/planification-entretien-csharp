using System.Collections.Generic;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.entretien.infrastructure.repository;
using PlanificationEntretien.entretien.query.application;

namespace PlanificationEntretien.entretien.application_service.infrastructure;

public class InMemoryEntretienDao : IEntretienDao, IEntretienProjectionDao
{
    private readonly InMemoryEntretienRepository _repository;
    private readonly Dictionary<Candidat,InMemoryEntretien> _entretiens;
    private int count;

    public InMemoryEntretienDao(InMemoryEntretienRepository repository)
    {
        _entretiens = repository.Entretiens;
    }

    public IEnumerable<IEntretien> FindAll()
    {
        return _entretiens.Values;
    }

    public void IncrementEntretienAnnule()
    {
        count++;
    }

    public int EntretiensAnnules()
    {
        return count;
    }
}