namespace Cqrs;

public interface QueryHandler<Q, R>
    where Q : Query
    where R : QueryResponse<Type>
{
    R handle(Q query);

    Type listenTo();
}