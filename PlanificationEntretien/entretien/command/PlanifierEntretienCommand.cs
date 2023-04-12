using System;
using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.entretien.application_service;

public record PlanifierEntretienCommand(Candidat Candidat,
    DateTime DisponibiliteDuCandidat,
    Recruteur Recruteur,
    DateTime DisponibiliteDuRecruteur) : ICommand;