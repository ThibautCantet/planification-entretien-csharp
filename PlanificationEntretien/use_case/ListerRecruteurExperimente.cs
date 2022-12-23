using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.use_case;

public class ListerRecruteurExperimente
{
    private readonly IRecruteurPort _recruteurPort;
    
    public ListerRecruteurExperimente(IRecruteurPort recruteurPort)
    {
        _recruteurPort = recruteurPort;
    }

    public List<Recruteur> Execute()
    {
        List<Recruteur> recruteurs = _recruteurPort.FindAll()
            .Where(r => r.ExperienceEnAnnees >= 10)
            .ToList();

        return recruteurs;
    } 
}