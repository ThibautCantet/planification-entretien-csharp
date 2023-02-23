using System.Collections.Generic;
using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.entretien.application_service;

public class ListerEntretienQueryHandler
{
    private readonly IEntretienRepository _entretienRepository;

    public ListerEntretienQueryHandler(IEntretienRepository entretienRepository)
    {
        _entretienRepository = entretienRepository;
    }
    
    public IEnumerable<IEntretien> Handle(ListerEntretienQuery query)
    {
        return _entretienRepository.FindAll();
    }
}