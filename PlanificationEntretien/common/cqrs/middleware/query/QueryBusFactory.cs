using PlanificationEntretien.common.cqrs.middleware.query;
using PlanificationEntretien.recruteur.application_service.application;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.entretien.query.application;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class QueryBusFactory
{
    private readonly IEntretienDao _entretienDao;
    private readonly IRecruteurDao _recruteurDao;
    private readonly IEntretienProjectionDao _entretienProjectionDao;

    public QueryBusFactory(IEntretienDao entretienDao, IRecruteurDao recruteurDao, IEntretienProjectionDao entretienProjectionDao)
    {
        _entretienDao = entretienDao;
        _recruteurDao = recruteurDao;
        _entretienProjectionDao = entretienProjectionDao;
    }


    public IQueryBus Build()
    {
        return new QueryBusDispatcher(_entretienDao, _recruteurDao, _entretienProjectionDao);
    }
}
