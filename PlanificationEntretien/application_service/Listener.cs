using Planification_Entretien.domain;

namespace PlanificationEntretien.application_service;

public interface Listener
{
    void OnMessage(Event msg);
}