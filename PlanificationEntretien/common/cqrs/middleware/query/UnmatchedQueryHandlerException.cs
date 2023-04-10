using System;
using PlanificationEntretien.common.cqrs.query;

namespace PlanificationEntretien.common.cqrs.middleware.query;
public class UnmatchedQueryHandlerException : Exception
{
    public UnmatchedQueryHandlerException(IQuery query)
        : base($"No matching query handler found for query of type {query.GetType().FullName}")
    {
    }
}