namespace Cqrs;

public class CommandResponse<Event>
{

    private List<Event> _events;
    
    public CommandResponse(Event e) {
        _events = new List<Event>();
        _events.Add(e);
    }

    public Boolean containEventType(Type clazz) {
        return _events.ToList().Exists(e => e.GetType() == clazz);
    }

    public List<Event> events() {
        return _events;
    }
}
