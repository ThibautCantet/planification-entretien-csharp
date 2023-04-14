using com.soat.planification_entretien.common.cqrs.command;

namespace PlanificationEntretien.entretien.application_service;

public record AnnulerEntretienCommand(int Id) : ICommand;