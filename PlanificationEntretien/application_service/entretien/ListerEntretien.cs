using System.Collections.Generic;
using PlanificationEntretien.domain.entretien;

namespace PlanificationEntretien.application_service.entretien;

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