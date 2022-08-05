using Cqrs;

public interface EventBus
{
    Command publish(Event e);

    void resetPublishedEvents();

    HashSet<Event> getPublishedEvents();
}
