using System;
using System.Collections.Generic;
using System.Linq;
using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.domain;
using PlanificationEntretien.entretien.application_service;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.recruteur.application_service.application;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.common.cqrs.middleware.evt;

public class EventBusDispatcher : IEventBus
{
    private readonly HashSet<Event> _publishedEvents;
    private readonly IRecruteurRepository _recruteurRepository;
    private readonly IRecruteurDao _recruteurDao;

    public EventBusDispatcher(IRecruteurDao recruteurDao, IRecruteurRepository recruteurRepository)
    {
        _recruteurDao = recruteurDao;
        _recruteurRepository = recruteurRepository;
        this._publishedEvents = new();
    }

    public ICommand Publish(Event @event)
    {
        IEnumerable<IEventHandler> eventHandlers = GetListeners(@event);

        var commands = new List<ICommand>();
        foreach (var handler in eventHandlers)
        {
            switch (handler.GetHandlerType())
            {
                case EventHandlerType.COMMAND:
                    //TODO handle selon le type
                    //var command = ((IEventHandlerReturnCommand<>)handler).Handle(@event);
                    //if (command != null)
                    //{
                    //    commands.Add(command);
                    //}
                    break;
                case EventHandlerType.EVENT:
                    //TODO handle selon le type
                    //var newEvent = ((IEventHandlerReturnEvent)handler).Handle(@event);
                    //if (newEvent != null)
                    //{
                    //    Publish(newEvent);
                    //    _publishedEvents.Add(newEvent);
                    //}
                    break;
                case EventHandlerType.VOID:
                    Type eventHandlerType = handler.GetType();
                    if (eventHandlerType == typeof(EntretienCréeListener)) {
                        var entretienCréeListener = handler as EntretienCréeListener;
                        entretienCréeListener.Handle(@event as EntretienCréé);
                    }
                    if (eventHandlerType == typeof(RecruteurCréeListener)) {
                        var recruteurCréeListener = handler as RecruteurCréeListener;
                        recruteurCréeListener.Handle(@event as RecruteurCrée);
                    }
                    //((IEventHandlerReturnVoid)handler).Handle(@event);
                    break;
            }
        }

        return commands.FirstOrDefault();
    }

    public void ResetPublishedEvents()
    {
        _publishedEvents.Clear();
    }

    public HashSet<Event> GetPublishedEvents()
    {
        return _publishedEvents;
    }

    private IEnumerable<IEventHandler> GetListeners(Event @event)
    {
        if (@event.GetType() == typeof(EntretienCréé))
        {
            yield return new EntretienCréeListener(new RendreRecruteurIndisponibleCommandHandler(_recruteurRepository));
        }
        if (@event.GetType() == typeof(RecruteurCrée))
        {
            yield return new RecruteurCréeListener(_recruteurDao);
        }
    }
}
