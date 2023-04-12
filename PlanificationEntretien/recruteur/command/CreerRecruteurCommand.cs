using com.soat.planification_entretien.common.cqrs.command;

namespace PlanificationEntretien.recruteur.application_service;

public record CreerRecruteurCommand(string Language, string Email, int? ExperienceEnAnnees) : ICommand;