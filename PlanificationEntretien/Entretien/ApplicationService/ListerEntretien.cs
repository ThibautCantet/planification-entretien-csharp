using System.Collections.Generic;
using PlanificationEntretien.Entretien.Domain;

namespace PlanificationEntretien.Entretien.ApplicationService;

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