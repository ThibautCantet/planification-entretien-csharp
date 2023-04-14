using System;
using com.soat.planification_entretien.common.cqrs.query;
using PlanificationEntretien.entretien.query.application;

namespace PlanificationEntretien.entretien.application_service;

public class CompterEntretiensAnnulesQueryHandler : IQueryHandler<CompterEntretiensAnnulesQuery, QueryResponse<int>, int>
{
    private readonly IEntretienProjectionDao _entretienProjectionDao;

    public CompterEntretiensAnnulesQueryHandler(IEntretienProjectionDao entretienProjectionDao)
    {
        _entretienProjectionDao = entretienProjectionDao;
    }
    
    public QueryResponse<int> Handle(CompterEntretiensAnnulesQuery query)
    {
        return new QueryResponse<int>(_entretienProjectionDao.EntretiensAnnules(), new EntretiensAnnulésComptés());
    }

    public Type ListenTo()
    {
        return typeof(CompterEntretiensAnnulesQuery);
    }
}