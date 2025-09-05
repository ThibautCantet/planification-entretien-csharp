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
    private readonly ListerEntretien _listerEntretien;
    private readonly ValiderEntretien _validerEntretien;
    private readonly AnnulerEntretien _annulerEntretien;

    public EntretienController(PlanifierEntretien planifierEntretien,
        ListerEntretien listerEntretien,
        ValiderEntretien validerEntretien,
        AnnulerEntretien annulerEntretien)
    {
        _planifierEntretien = planifierEntretien;
        _listerEntretien = listerEntretien;
        _validerEntretien = validerEntretien;
        _annulerEntretien = annulerEntretien;
    }


    [HttpPost]
    public ActionResult Create([FromBody] CreateEntretienRequest createOfferRequest)
    {

        var planificationResult = _planifierEntretien.Execute(
            createOfferRequest.IdCandidat,
            createOfferRequest.DisponibiliteCandidat,
            createOfferRequest.DisponibiliteRecruteur);

        if (planificationResult is EntretienPlanifie entretienPlanifie)
        {
            var response = new CreateEntretienResponse(entretienPlanifie.EntretienId, entretienPlanifie.CandidatEmail, entretienPlanifie.RecruteurEmail, createOfferRequest.DisponibiliteCandidat);
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
        var result = _validerEntretien.Execute(entretienId);
        if (result is ValidationEntretienEchoue entretienEchoue)
        {
            return BadRequest();
        }

        return Ok();
    }

    public IActionResult Annuler(int entretienId)
    {
        _annulerEntretien.Execute(entretienId);
            return Ok();
    }
}