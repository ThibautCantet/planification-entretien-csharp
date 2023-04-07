using System.Collections.Generic;
using PlanificationEntretien.recruteur.application_service.application;

namespace PlanificationEntretien.recruteur.application_service;

public class ListerRecruteurExperimenteQueryHandler
{
    private readonly IRecruteurDao _recruteurDao;
    
    public ListerRecruteurExperimenteQueryHandler(IRecruteurDao recruteurDao)
    {
        _recruteurDao = recruteurDao;
    }

    public List<RecruteurDetail> Handle(ListerRecruteurExperimenteQuery query)
    {
        return _recruteurDao.Find10AnsExperience();
    } 
}