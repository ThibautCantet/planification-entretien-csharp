using System.Collections.Generic;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.use_case;

public class ListerEntretien
{
    private readonly IEntretienPort _entretienPort;

    public ListerEntretien(IEntretienPort entretienPort)
    {
        _entretienPort = entretienPort;
    }
    
    public IEnumerable<IEntretien> Execute()
    {
        return _entretienPort.FindAll();
    }
}