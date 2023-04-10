using System;
using System.Collections.Generic;
using System.Linq;
using com.soat.planification_entretien.common.cqrs.query;
using PlanificationEntretien.common.cqrs.middleware.command;
using PlanificationEntretien.common.cqrs.query;

namespace PlanificationEntretien.common.cqrs.middleware.query;

public class QueryBusDispatcher : IQueryBus
{
    private readonly Dictionary<Type, IQueryHandler> _queryHandlers;

    public QueryBusDispatcher(IEnumerable<IQueryHandler> queryHandlers)
    {
        this._queryHandlers = queryHandlers.ToDictionary(handler => handler.ListenTo(), handler => handler);
    }

    public QueryResponse Dispatch(IQuery query)
    {
        Type queryType = query.GetType();
        if (_queryHandlers.ContainsKey(queryType))
        {
            var commandHandler = _queryHandlers[queryType];
            return commandHandler.Handle(query);
        }

        throw new UnmatchedQueryHandlerException(query);
    }
}
