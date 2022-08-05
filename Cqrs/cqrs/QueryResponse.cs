namespace Cqrs;

public record QueryResponse<T>(T value, Event e)
{
    // Todo(voir comment passer e à null ?)
    //public QueryResponse(Event e) : this(null, e)
    //{
    //}
}