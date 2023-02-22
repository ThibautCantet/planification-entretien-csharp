using Planification_Entretien.domain;

namespace PlanificationEntretien.domain.entretien;

public record EntretienCréé(int EntretienId, int RecruteurId) : Event
{
    public EntretienCréé UpdateId(int entretienId)
    {
        return this with { EntretienId = entretienId };
    }
}