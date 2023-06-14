using System;
using System.Collections.Generic;
using PlanificationEntretien.repository;
using PlanificationEntretien.model;
using service;

namespace PlanificationEntretien.service;

public class EntretienService
{
    private readonly EntretienRepository _entretienRepository;
    private readonly CandidatRepository _candidatRepository;
    private readonly RecruteurRepository _recruteurRepository;
    private readonly IEmailService _emailService;

    public EntretienService(EntretienRepository entretienRepository, IEmailService emailService,
        CandidatRepository candidatRepository, RecruteurRepository recruteurRepository)
    {
        this._entretienRepository = entretienRepository;
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