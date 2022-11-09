using System;
using PlanificationEntretien.domain;
using PlanificationEntretien.email;

namespace PlanificationEntretien.use_case;

public class PlanifierEntretien
{
    private readonly IEntretienPort _entretienPort;
    private readonly ICandidatPort _candidatPort;
    private readonly IRecruteurPort _recruteurPort;
    private readonly IEmailPort _emailPort;

    public PlanifierEntretien(IEntretienPort entretienPort, IEmailPort emailPort,
        ICandidatPort candidatPort, IRecruteurPort recruteurPort)
    {
        _entretienPort = entretienPort;
        _emailPort = emailPort;
        _candidatPort = candidatPort;
        _recruteurPort = recruteurPort;
    }

    public Boolean Execute(string emailCandidat, DateTime disponibiliteDuCandidat,
        string emailRecruteur, DateTime dateDeDisponibiliteDuRecruteur)
    {
        var candidat = _candidatPort.FindByEmail(emailCandidat);
        var recruteur = _recruteurPort.FindByEmail(emailRecruteur);
        if (candidat.Language.Equals(recruteur.Language)
            && candidat.ExperienceEnAnnees < recruteur.ExperienceEnAnnees
            && disponibiliteDuCandidat.Equals(dateDeDisponibiliteDuRecruteur))
        {
            Entretien entretien = new Entretien(candidat, recruteur, dateDeDisponibiliteDuRecruteur);
            _entretienPort.Save(entretien);
            _emailPort.EnvoyerUnEmailDeConfirmationAuCandidat(candidat.Email, dateDeDisponibiliteDuRecruteur);
            _emailPort.EnvoyerUnEmailDeConfirmationAuRecruteur(recruteur.Email, dateDeDisponibiliteDuRecruteur);
            return true;
        }

        return false;
    }
}