using Shared;

namespace PlanificationEntretien.domain.entretien;

public record PlanificationEntretienEchoué(Entretien Entretien) : Event;