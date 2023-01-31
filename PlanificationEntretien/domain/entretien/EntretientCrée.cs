using Planification_Entretien.domain;

namespace PlanificationEntretien.domain.entretien;

public record EntretienCréé(int EntretienId, int RecruteurId) : Event;