using System;
using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.entretien.application_service;

public interface IEntretien
{
    int Id();

    String EmailCandidat();

    String EmailRecruteur();

    String Language();

    DateTime Horaire();

    Status Status();
}