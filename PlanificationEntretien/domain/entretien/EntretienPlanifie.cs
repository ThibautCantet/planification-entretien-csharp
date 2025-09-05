using Shared;

namespace PlanificationEntretien.domain.entretien;

public record EntretienPlanifie(int EntretienId, string CandidatEmail, string RecruteurEmail) : Event;