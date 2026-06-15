using PlanificationEntretien.Common.Domain;

namespace PlanificationEntretien.Common.ApplicationService;

public interface Listener
{
    void OnMessage(Event msg);
}