using System;
using System.Collections.Generic;
using PlanificationEntretien.email;
using PlanificationEntretien.memory;
using PlanificationEntretien.model;

namespace PlanificationEntretien.service;

public class EntretienService
{
    private readonly IEntretienRepository _entretienRepository;
    private readonly ICandidatRepository _candidatRepository;
    private readonly IRecruteurRepository _recruteurRepository;
    private readonly IEmailService _emailService;

    public EntretienService(IEntretienRepository entretienRepository, IEmailService emailService,
        ICandidatRepository candidatRepository, IRecruteurRepository recruteurRepository)
    {
        _entretienRepository = entretienRepository;
        _emailService = emailService;
        _candidatRepository = candidatRepository;
        _recruteurRepository = recruteurRepository;
    }

    public Boolean Planifier(string emailCandidat, DateTime disponibiliteDuCandidat,
        string emailRecruteur, DateTime dateDeDisponibiliteDuRecruteur)
    {
        var candidat = _candidatRepository.FindByEmail(emailCandidat);
        var recruteur = _recruteurRepository.FindByEmail(emailRecruteur);
        if (candidat.Language.Equals(recruteur.Language)
            && candidat.ExperienceEnAnnees < recruteur.ExperienceEnAnnees
            && disponibiliteDuCandidat.Equals(dateDeDisponibiliteDuRecruteur))
        {
            Entretien entretien = new Entretien(candidat, recruteur, dateDeDisponibiliteDuRecruteur);
            _entretienRepository.Save(entretien);
            _emailService.EnvoyerUnEmailDeConfirmationAuCandidat(candidat.Email, dateDeDisponibiliteDuRecruteur);
            _emailService.EnvoyerUnEmailDeConfirmationAuRecruteur(recruteur.Email, dateDeDisponibiliteDuRecruteur);
            return true;
        }

        return false;
    }

    public IEnumerable<Entretien> ListerEntretiens()
    {
        return _entretienRepository.FindAll();
    }
}