using System;
using PlanificationEntretien.common.cqrs.query;

namespace PlanificationEntretien.Common.Cqrs.Middleware.Query;
public class UnmatchedQueryHandlerException : Exception
{
    public UnmatchedQueryHandlerException(IQuery query)
        : base($"No matching query handler found for query of type {query.GetType().FullName}")
    {
    }
}