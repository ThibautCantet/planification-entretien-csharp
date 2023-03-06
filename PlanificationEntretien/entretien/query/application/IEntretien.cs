using System;
using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.entretien.application_service;

public interface IEntretien
{
    int Id();

    string EmailCandidat();

    string EmailRecruteur();

    string Language();

    DateTime Horaire();

    Status Status();
}