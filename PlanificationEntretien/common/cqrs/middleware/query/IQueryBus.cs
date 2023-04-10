using com.soat.planification_entretien.common.cqrs.query;
using PlanificationEntretien.common.cqrs.query;

namespace PlanificationEntretien.Common.Cqrs.Middleware.Query;

public interface IQueryBus
{
    IQueryResponse<T> Dispatch<T>(IQuery query);
}