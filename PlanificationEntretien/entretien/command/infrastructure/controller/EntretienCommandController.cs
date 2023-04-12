using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.candidat.domain;
using PlanificationEntretien.recruteur.domain;
using PlanificationEntretien.entretien.application_service;
using PlanificationEntretien.entretien.domain;
using Candidat = PlanificationEntretien.entretien.domain.Candidat;
using Recruteur = PlanificationEntretien.entretien.domain.Recruteur;

namespace PlanificationEntretien.entretien.infrastructure.controller;

[ApiController]
[Route("/api/entretien")]
public class EntretienCommandController : ControllerBase
{
    private readonly PlanifierEntretienCommandHandler _planifierEntretienCommandHandler;
    private readonly ICandidatRepository _candidatRepository;
    private readonly IRecruteurRepository _recruteurRepository;
    private readonly ValiderEntretienCommandHandler _validerEntretienCommandHandler;

    public EntretienCommandController(PlanifierEntretienCommandHandler planifierEntretienCommandHandler, ValiderEntretienCommandHandler validerEntretienCommandHandler,
        ICandidatRepository candidatRepository, IRecruteurRepository recruteurRepository)
    {
        _planifierEntretienCommandHandler = planifierEntretienCommandHandler;
        _candidatRepository = candidatRepository;
        _recruteurRepository = recruteurRepository;
        _validerEntretienCommandHandler = validerEntretienCommandHandler;
    }

    
    [HttpPost]
    public ActionResult Create([FromBody] CreateEntretienRequest createOfferRequest)
    {
        var candidat = _candidatRepository.FindById(createOfferRequest.IdCandidat);
        var recruteur = _recruteurRepository.FindById(createOfferRequest.IdRecruteur);
        var events = _planifierEntretienCommandHandler.Handle(new PlanifierEntretienCommand(
            new Candidat(candidat.Id, candidat.Language, candidat.Email, candidat.ExperienceEnAnnees),
            createOfferRequest.DisponibiliteCandidat,
            new Recruteur(recruteur.Id, recruteur.Language, recruteur.Email, recruteur.ExperienceEnAnnees),
            createOfferRequest.DisponibiliteRecruteur));
        EntretienCréé entretienCrée = events.FindFirst(typeof(EntretienCréé)) as EntretienCréé;
        if (entretienCrée != null)
        {
            var response = new CreateEntretienResponse(entretienCrée.EntretienId, candidat.Email, recruteur.Email,
                createOfferRequest.DisponibiliteCandidat);
            return CreatedAtAction("Create", new {id= createOfferRequest}, response);
        }
        return BadRequest();
    }

    public IActionResult Valider(int id)
    {
        if (_validerEntretienCommandHandler.Handle(new ValiderEntretienCommand(id)) == null)
        {
            return Ok();
        }

        return null;
    }
}