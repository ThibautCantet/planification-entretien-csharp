using com.soat.planification_entretien.common.cqrs.query;
using PlanificationEntretien.common.cqrs.middleware.command;
using PlanificationEntretien.common.cqrs.query;
using PlanificationEntretien.entretien.application_service;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.entretien.query.application;
using PlanificationEntretien.recruteur.application_service;
using PlanificationEntretien.recruteur.application_service.application;

namespace PlanificationEntretien.common.cqrs.middleware.query;

public class QueryBusDispatcher : IQueryBus
{
    private readonly IEntretienDao _entretienDao;
    private readonly IRecruteurDao _recruteurDao;
    private readonly IEntretienProjectionDao _entretienProjectionDao;

    public QueryBusDispatcher(IEntretienDao entretienDao, IRecruteurDao recruteurDao, IEntretienProjectionDao entretienProjectionDao)
    {
        _entretienDao = entretienDao;
        _recruteurDao = recruteurDao;
        _entretienProjectionDao = entretienProjectionDao;
    }

    public IQueryResponse<T> Dispatch<T>(IQuery query)
    {
        if (query.GetType() == typeof(ListerEntretienQuery))
        {
            return (IQueryResponse<T>)new ListerEntretienQueryHandler(_entretienDao).Handle(query as ListerEntretienQuery);
        }
        if (query.GetType() == typeof(ListerRecruteurExperimenteQuery))
        {
            return (IQueryResponse<T>)new ListerRecruteurExperimenteQueryHandler(_recruteurDao).Handle(query as ListerRecruteurExperimenteQuery);
        }
        if (query.GetType() == typeof(CompterEntretiensAnnulesQuery))
        {
            return (IQueryResponse<T>)new CompterEntretiensAnnulesQueryHandler(_entretienProjectionDao).Handle(query as CompterEntretiensAnnulesQuery);
        }
        throw new UnmatchedQueryHandlerException(query);
    }
}
