using PlanificationEntretien.domain;

namespace PlanificationEntretien.application_service;

public interface Listener
{
    void OnMessage(Event msg);
}