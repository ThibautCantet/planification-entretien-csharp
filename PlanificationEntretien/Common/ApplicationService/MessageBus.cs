using System.Collections.Generic;
using PlanificationEntretien.Common.Domain;

namespace PlanificationEntretien.Common.ApplicationService;

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