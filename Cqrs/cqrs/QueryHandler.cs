namespace Cqrs;

public interface QueryHandler<Query, QueryResponse>
{
    QueryResponse handle(Query query);

    Type listenTo();
}