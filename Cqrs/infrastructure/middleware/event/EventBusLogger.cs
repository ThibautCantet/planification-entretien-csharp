using Cqrs;

public class EventBusLogger : EventBus
{
    private EventBus eventBus;

    public EventBusLogger(EventBus eventBus)
    {
        this.eventBus = eventBus;
    }

    public Command publish(Event e)
    {
        Command command = eventBus.publish(e);
        if (e != null)
        {
            Console.Out.WriteLine(e);
        }

        return command;
    }

    public void resetPublishedEvents()
    {
        eventBus.resetPublishedEvents();
    }

    public HashSet<Event> getPublishedEvents()
    {
        return eventBus.getPublishedEvents();
    }
}