using com.soat.planification_entretien.common.cqrs.command;

namespace PlanificationEntretien.candidat.application_service;

public record CreerCandidatCommand(string Language, string Email, int? ExperienceEnAnnees) : ICommand;