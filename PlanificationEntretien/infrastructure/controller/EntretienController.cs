using System;
using System.Linq;
using Candidat.domain;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.domain.entretien;
using PlanificationEntretien.domain.recruteur;
using PlanificationEntretien.use_case;

namespace PlanificationEntretien.infrastructure.controller;

[ApiController]
[Route("/api/entretien")]
public class EntretienController : ControllerBase
{
    private readonly PlanifierEntretien _planifierEntretien;
    private readonly ICandidatRepository _candidatRepository;
    private readonly IRecruteurRepository _recruteurRepository;
    private readonly ListerEntretien _listerEntretien;
    private readonly ValiderEntretien _validerEntretien;

    public EntretienController(PlanifierEntretien planifierEntretien, ListerEntretien listerEntretien,ValiderEntretien validerEntretien,
        ICandidatRepository candidatRepository, IRecruteurRepository recruteurRepository
        )
    {
        _planifierEntretien = planifierEntretien;
        _candidatRepository = candidatRepository;
        _recruteurRepository = recruteurRepository;
        _listerEntretien = listerEntretien;
        _validerEntretien = validerEntretien;
    }


    [HttpPost]
    public ActionResult Create([FromBody] CreateEntretienRequest createOfferRequest)
    {
        var candidat = _candidatRepository.FindById(createOfferRequest.IdCandidat);
        var recruteur = _recruteurRepository.FindById(createOfferRequest.IdRecruteur);
        var entretienId = _planifierEntretien.Execute(
            new CandidatEvalué(candidat.Id, candidat.Language, candidat.Email, candidat.ExperienceEnAnnees),
            createOfferRequest.DisponibiliteCandidat,
            new RecruteurAssigné(recruteur.Id, recruteur.Language, recruteur.Email, recruteur.ExperienceEnAnnees),
            createOfferRequest.DisponibiliteRecruteur);
        if (entretienId > 0)
        {
            var response = new CreateEntretienResponse(entretienId, candidat.Email, recruteur.Email,
                createOfferRequest.DisponibiliteCandidat);
            return CreatedAtAction("Create", new {id= createOfferRequest}, response);
        }
        return BadRequest();
    }

    public IActionResult Lister()
    {
        var entretiens = _listerEntretien.Execute()
            .Select(entretien => new EntretienResponse(entretien.CandidatEvalué.Email,
                entretien.RecruteurAssigné.Email,
                entretien.Horaire,
                entretien.Status))
            .ToList();
        return Ok(entretiens);
    }

    public IActionResult Valider(int entretienId)
    {
        _validerEntretien.Execute(entretienId);
        return Ok();
    }
}