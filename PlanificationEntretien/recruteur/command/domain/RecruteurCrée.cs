using PlanificationEntretien.domain;

namespace PlanificationEntretien.recruteur.domain;

public record RecruteurCrée(int Id, string Language, int ExperienceInYears, string Email) : Event;
