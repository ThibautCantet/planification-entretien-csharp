using Cqrs;
using EventHandler = Cqrs.EventHandler;

public class EventBusDispatcher : EventBus
{
    private Dictionary<Type, List<EventHandler>> _eventHandlers;
    private HashSet<Event> _publishedEvents;

    public EventBusDispatcher(List<EventHandler> eventHandlers)
    {
        _eventHandlers = eventHandlers
            .GroupBy(x => x.listenTo())
            .ToDictionary(x => x.Key, x => x.ToList());

        _publishedEvents = new HashSet<Event>();
    }

    public Command publish(Event e)
    {
        //List<? extends EventHandler> eventHandlers = getListeners(e);
        var eventHandlers = getListeners(e);

        var commands = new List<Command>();
        foreach (var handler in eventHandlers)
        {
            switch (handler.getType())
            {
                case EventHandlerType.COMMAND:
                    Command command = ((EventHandlerCommand)handler).handle(e);
                    if (command != null)
                    {
                        commands.Add(command);
                    }

                    break;
                case EventHandlerType.EVENT:
                    Event newEvent = ((EventHandlerEvent)handler).handle(e);
                    if (newEvent != null)
                    {
                        publish(newEvent);
                        _publishedEvents.Add(newEvent);
                    }

                    break;
                case EventHandlerType.VOID:
                    ((EventHandlerVoid)handler).handle(e);
                    break;
            }
        }

        return commands.First();
    }

    public void resetPublishedEvents()
    {
        _publishedEvents.Clear();
    }

    private List<EventHandler> getListeners(Event e)
    {
        return (List<EventHandler>)_eventHandlers
            .Where(entry => entry.Key.GetType() == e.GetType())
            .SelectMany(classEntry => classEntry.Value);
    }

    public HashSet<Event> getPublishedEvents()
    {
        return _publishedEvents;
    }
}