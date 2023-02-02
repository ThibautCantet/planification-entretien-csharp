using System.Collections.Generic;
using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.entretien.application_service;

public class ListerEntretien
{
    private readonly IEntretienRepository _entretienRepository;

    public ListerEntretien(IEntretienRepository entretienRepository)
    {
        _entretienRepository = entretienRepository;
    }
    
    public IEnumerable<IEntretien> Execute()
    {
        return _entretienRepository.FindAll();
    }
}