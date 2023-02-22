using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.recruteur.application_service;

public class ListerRecruteurExperimenteQueryHandler
{
    private readonly IRecruteurRepository _recruteurRepository;
    
    public ListerRecruteurExperimenteQueryHandler(IRecruteurRepository recruteurRepository)
    {
        _recruteurRepository = recruteurRepository;
    }

    public List<Recruteur> Handle()
    {
        List<Recruteur> recruteurs = _recruteurRepository.FindAll()
            .Where(r => r.ExperienceEnAnnees >= 10)
            .ToList();

        return recruteurs;
    } 
}