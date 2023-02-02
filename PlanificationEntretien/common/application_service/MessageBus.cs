using System.Collections.Generic;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.application_service;

public class MessageBus
{
    private List<Listener> subs = new();

    public void Subscribe(Listener l) {
        subs.Add(l);
    }

    public void Send(Event msg) {
        foreach (var l in subs) {
            l.OnMessage(msg);
        }
    }
}