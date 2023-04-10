using System;
using PlanificationEntretien.common.cqrs.query;

namespace com.soat.planification_entretien.common.cqrs.query;


public interface IQueryHandler
{
    QueryResponse Handle(IQuery query);

    Type ListenTo();
}