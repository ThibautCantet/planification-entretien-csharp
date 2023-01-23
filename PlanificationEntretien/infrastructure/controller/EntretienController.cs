using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.domain.candidat;
using PlanificationEntretien.domain.recruteur;
using PlanificationEntretien.use_case;
using Candidat = PlanificationEntretien.domain.entretien.Candidat;

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
        var entretienId = _planifierEntretien.Execute(
            new Candidat(candidat.Id, candidat.Language, candidat.Email, candidat.ExperienceEnAnnees),
            createOfferRequest.DisponibiliteCandidat,
            recruteur, createOfferRequest.DisponibiliteRecruteur);
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
            .Select(entretien => new EntretienResponse(entretien.Candidat.Email,
                entretien.Recruteur.Email,
                entretien.Horaire))
            .ToList();
        return Ok(entretiens);
    }
}