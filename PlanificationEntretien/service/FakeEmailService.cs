using service;
using System;

namespace PlanificationEntretien.email;

public class FakeEmailService : IEmailService
{
    private bool _unEmailDeConfirmationAEteEnvoyeAuCandidat;
    private bool _unEmailDeConfirmationAEteEnvoyeAuRecruteur;

    public bool UnEmailDeConfirmationAEteEnvoyeAuCandidat(string candidatEmail, DateTime horaire)
    {
        return _unEmailDeConfirmationAEteEnvoyeAuCandidat;
    }

    public bool UnEmailDeConfirmationAEteEnvoyeAuRecruteur(string recruteurEmail, DateTime horaire)
    {
        return _unEmailDeConfirmationAEteEnvoyeAuRecruteur;
    }

    public void EnvoyerUnEmailDeConfirmationAuCandidat(string email, DateTime horaire)
    {
        _unEmailDeConfirmationAEteEnvoyeAuCandidat = true;
    }

    public void EnvoyerUnEmailDeConfirmationAuRecruteur(string email, DateTime horaire)
    {
        _unEmailDeConfirmationAEteEnvoyeAuRecruteur = true;
    }
}