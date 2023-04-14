using PlanificationEntretien.common.cqrs.middleware.query;
using PlanificationEntretien.recruteur.application_service.application;
using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public class QueryBusFactory
{
    private readonly IEntretienDao _entretienDao;
    private readonly IRecruteurDao _recruteurDao;

    public QueryBusFactory(IEntretienDao entretienDao, IRecruteurDao recruteurDao)
    {
        _entretienDao = entretienDao;
        _recruteurDao = recruteurDao;
    }


    public IQueryBus Build()
    {
        return new QueryBusDispatcher(_entretienDao, _recruteurDao);
    }
}
