using System;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.candidat.infrastructure.repository;
using PlanificationEntretien.entretien.application_service;
using PlanificationEntretien.recruteur.infrastructure.repository;

namespace PlanificationEntretien.entretien.infrastructure.repository;

public record InMemoryEntretien(
    int Id,
    InMemoryCandidat Candidat,
    InMemoryRecruteur Recruteur,
    DateTime Horaire,
    Status Status): IEntretien
{
    int IEntretien.Id()
    {
        return Id;
    }

    string IEntretien.EmailCandidat()
    {
        return Candidat.Email;
    }

    string IEntretien.EmailRecruteur()
    {
        return Recruteur.Email;
    }

    string IEntretien.Language()
    {
        return Recruteur.Language;
    }

    DateTime IEntretien.Horaire()
    {
        return Horaire;
    }

    Status IEntretien.Status()
    {
        return Status;
    }
}