using PlanificationEntretien.candidat.infrastructure.repository;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.entretien.infrastructure.repository;
using PlanificationEntretien.recruteur.infrastructure.repository;

namespace PlanificationEntretien.Steps;

public abstract class ATest
{
    private InMemoryEntretienRepository _inMemoryEntretienRepository;
    protected IEntretienRepository EntretienRepository()
    {
        if (_inMemoryEntretienRepository == null)
        {
            _inMemoryEntretienRepository = new();
        }
        return _inMemoryEntretienRepository;
    }

    private InMemoryRecruteurRepository _inMemoryRecruteurRepository;
    protected InMemoryRecruteurRepository RecruteurRepository()
    {
        if (_inMemoryRecruteurRepository == null)
        {
            _inMemoryRecruteurRepository = new();
        }
        return _inMemoryRecruteurRepository;
    }
    
    private InMemoryRecruteurDao _inMemoryRecruteurDao;
    protected InMemoryRecruteurDao RecruteurDao()
    {
        if (_inMemoryRecruteurDao == null)
        {
            _inMemoryRecruteurDao = new InMemoryRecruteurDao(RecruteurRepository());
        }
        return _inMemoryRecruteurDao;
    }
    
    private InMemoryCandidatRepository _inMemoryCandidatRepository;
    protected InMemoryCandidatRepository CandidatRepository()
    {
        if (_inMemoryCandidatRepository == null)
        {
            _inMemoryCandidatRepository = new();
        }
        return _inMemoryCandidatRepository;
    }
}