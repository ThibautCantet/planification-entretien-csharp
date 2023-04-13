using PlanificationEntretien.application_service;
using PlanificationEntretien.candidat.domain_service;
using PlanificationEntretien.candidat.infrastructure.repository;
using PlanificationEntretien.common.cqrs.middleware.command;
using PlanificationEntretien.entretien.application_service.infrastructure;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.entretien.infrastructure;
using PlanificationEntretien.entretien.infrastructure.repository;
using PlanificationEntretien.recruteur.infrastructure.repository;

namespace PlanificationEntretien.Steps;

public abstract class ATest
{
    protected readonly IEmailService _emailService = new FakeEmailService();
    protected MessageBus _messageBus = new ();

    private CommandBusFactory _commandBusFactory;
    protected CommandBusFactory CommandBusFactory()
    {
        if (_commandBusFactory == null)
        {
            _commandBusFactory = new(
                CandidatRepository(),
                new CandidatFactory(),
                EntretienRepository(),
                _emailService,
                _messageBus,
                RecruteurRepository(),
                RecruteurDao());
        }
        return _commandBusFactory;
    }
    
    private QueryBusFactory _queryBusFactory;
    protected QueryBusFactory QueryBusFactory()
    {
        if (_queryBusFactory == null)
        {
            _queryBusFactory = new(EntretienDao(),
                RecruteurDao());
        }
        return _queryBusFactory;
    }
 
    private InMemoryEntretienRepository _inMemoryEntretienRepository;
    protected InMemoryEntretienRepository EntretienRepository()
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
            _inMemoryRecruteurDao = new InMemoryRecruteurDao();
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
    
      
    private InMemoryEntretienDao _inMemoryEntretienDao;
    protected InMemoryEntretienDao EntretienDao()
    {
        if (_inMemoryEntretienDao == null)
        {
            _inMemoryEntretienDao = new InMemoryEntretienDao(EntretienRepository());
        }
        return _inMemoryEntretienDao;
    }
}