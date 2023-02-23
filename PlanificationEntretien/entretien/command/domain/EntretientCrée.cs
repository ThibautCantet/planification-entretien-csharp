using PlanificationEntretien.domain;

namespace PlanificationEntretien.entretien.domain;

public record EntretienCréé(int EntretienId, int RecruteurId) : Event
{
    public EntretienCréé UpdateId(int entretienId)
    {
        return this with { EntretienId = entretienId };
    }
}