using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.Recruteur.Domain;

namespace PlanificationEntretien.Recruteur.ApplicationService;

public class ListerRecruteurExperimente
{
    private readonly IRecruteurRepository _recruteurRepository;
    
    public ListerRecruteurExperimente(IRecruteurRepository recruteurRepository)
    {
        _recruteurRepository = recruteurRepository;
    }

    public List<Domain.Recruteur> Execute()
    {
        List<Domain.Recruteur> recruteurs = _recruteurRepository.FindAll()
            .Where(r => r.ExperienceEnAnnees >= 10)
            .ToList();

        return recruteurs;
    } 
}