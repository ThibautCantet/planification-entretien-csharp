using System;
using PlanificationEntretien.common.cqrs.query;

namespace com.soat.planification_entretien.common.cqrs.query;


public interface IQueryHandler<in Q, out R, T> where Q : IQuery where R : QueryResponse<T>
{
    R Handle(Q query);

    Type ListenTo();
}