using System.Collections.Generic;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.use_case;

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