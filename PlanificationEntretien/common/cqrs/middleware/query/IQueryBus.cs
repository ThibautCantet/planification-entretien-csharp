using com.soat.planification_entretien.common.cqrs.query;
using PlanificationEntretien.common.cqrs.query;

namespace PlanificationEntretien.common.cqrs.middleware.command;

public interface IQueryBus
{
    QueryResponse Dispatch(IQuery query);
}