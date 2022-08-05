namespace Cqrs;

public class CommandResponse<E>
    where E : Event 
{

    private List<E> _events;
    
    public CommandResponse(E e) {
        _events = new List<E>();
        _events.Add(e);
    }

    public Boolean containEventType(Type clazz) {
        return _events.ToList().Exists(e => e.GetType() == clazz);
    }

    public List<E> events() {
        return _events;
    }
}
