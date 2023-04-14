using System;
using System.Collections.Generic;
using com.soat.planification_entretien.common.cqrs.query;
using PlanificationEntretien.domain;
using PlanificationEntretien.recruteur.application_service.application;

namespace PlanificationEntretien.recruteur.application_service;

public class ListerRecruteurExperimenteQueryHandler : IQueryHandler<ListerRecruteurExperimenteQuery, QueryResponse<List<RecruteurDetail>>, List<RecruteurDetail>>
{
    private readonly IRecruteurDao _recruteurDao;
    
    public ListerRecruteurExperimenteQueryHandler(IRecruteurDao recruteurDao)
    {
        _recruteurDao = recruteurDao;
    }

    public QueryResponse<List<RecruteurDetail>> Handle(ListerRecruteurExperimenteQuery query)
    {
        return new QueryResponse<List<RecruteurDetail>>(_recruteurDao.Find10AnsExperience(), new RecruteursExperimentésListés());
    }

    public Type ListenTo()
    {
        return typeof(ListerRecruteurExperimenteQuery);
    }
}