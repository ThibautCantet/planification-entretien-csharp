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
public class EntretienController : ControllerBase
{
    private readonly PlanifierEntretienCommandHandler _planifierEntretienCommandHandler;
    private readonly ICandidatRepository _candidatRepository;
    private readonly IRecruteurRepository _recruteurRepository;
    private readonly ListerEntretienQueryHandler _listerEntretienQueryHandler;
    private readonly ValiderEntretienCommandHandler _validerEntretienCommandHandler;

    public EntretienController(PlanifierEntretienCommandHandler planifierEntretienCommandHandler, ListerEntretienQueryHandler listerEntretienQueryHandler, ValiderEntretienCommandHandler validerEntretienCommandHandler,
        ICandidatRepository candidatRepository, IRecruteurRepository recruteurRepository)
    {
        _planifierEntretienCommandHandler = planifierEntretienCommandHandler;
        _candidatRepository = candidatRepository;
        _recruteurRepository = recruteurRepository;
        _listerEntretienQueryHandler = listerEntretienQueryHandler;
        _validerEntretienCommandHandler = validerEntretienCommandHandler;
    }

    
    [HttpPost]
    public ActionResult Create([FromBody] CreateEntretienRequest createOfferRequest)
    {
        var candidat = _candidatRepository.FindById(createOfferRequest.IdCandidat);
        var recruteur = _recruteurRepository.FindById(createOfferRequest.IdRecruteur);
        var events = _planifierEntretienCommandHandler.Handle(
            new Candidat(candidat.Id, candidat.Language, candidat.Email, candidat.ExperienceEnAnnees),
            createOfferRequest.DisponibiliteCandidat,
            new Recruteur(recruteur.Id, recruteur.Language, recruteur.Email, recruteur.ExperienceEnAnnees),
            createOfferRequest.DisponibiliteRecruteur);
        EntretienCréé entretienCrée = events.FirstOrDefault(e => e.GetType().Equals(typeof(EntretienCréé))) as EntretienCréé;
        if (entretienCrée != null)
        {
            var response = new CreateEntretienResponse(entretienCrée.EntretienId, candidat.Email, recruteur.Email,
                createOfferRequest.DisponibiliteCandidat);
            return CreatedAtAction("Create", new {id= createOfferRequest}, response);
        }
        return BadRequest();
    }

    public IActionResult Lister()
    {
        var entretiens = _listerEntretienQueryHandler.Handle()
            .Select(entretien => new EntretienResponse(entretien.Candidat.Email,
                entretien.Recruteur.Email,
                entretien.Horaire,
                entretien.Status))
            .ToList();
        return Ok(entretiens);
    }

    public IActionResult Valider(int id)
    {
        if (_validerEntretienCommandHandler.Handle(id))
        {
            return Ok();
        }

        return null;
    }
}