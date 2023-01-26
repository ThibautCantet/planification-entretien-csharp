using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.domain;
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

    public EntretienController(PlanifierEntretien planifierEntretien, ListerEntretien listerEntretien,
        ICandidatRepository candidatRepository, IRecruteurRepository recruteurRepository)
    {
        _planifierEntretien = planifierEntretien;
        _candidatRepository = candidatRepository;
        _recruteurRepository = recruteurRepository;
        _listerEntretien = listerEntretien;
    }

    
    [HttpPost]
    public ActionResult Create([FromBody] CreateEntretienRequest createOfferRequest)
    {
        var candidat = _candidatRepository.FindById(createOfferRequest.IdCandidat);
        var recruteur = _recruteurRepository.FindById(createOfferRequest.IdRecruteur);
        var result = _planifierEntretien.Execute(candidat, createOfferRequest.DisponibiliteCandidat,
            recruteur, createOfferRequest.DisponibiliteRecruteur);
        if (result)
        {
            var response = new CreateEntretienResponse(candidat.Email, recruteur.Email,
                createOfferRequest.DisponibiliteCandidat);
            return CreatedAtAction("Create", new {id= createOfferRequest}, response);
        }
        return BadRequest();
    }

    public IActionResult Lister()
    {
        var entretiens = _listerEntretien.Execute()
            .Select(entretien => new EntretienResponse(entretien.Candidat.Email,
                entretien.Recruteur.Email,
                entretien.Horaire))
            .ToList();
        return Ok(entretiens);
    }
}