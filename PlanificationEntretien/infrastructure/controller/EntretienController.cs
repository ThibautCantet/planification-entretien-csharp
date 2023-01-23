using System.Threading.Tasks;
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

    public EntretienController(PlanifierEntretien planifierEntretien, ICandidatRepository candidatRepository, IRecruteurRepository recruteurRepository)
    {
        _planifierEntretien = planifierEntretien;
        _candidatRepository = candidatRepository;
        _recruteurRepository = recruteurRepository;
    }

    
    [HttpPost("")]
    public Task<IActionResult> Create([FromBody] CreateEntretienRequest createOfferRequest)
    {
        var candidat = _candidatRepository.FindByEmail(createOfferRequest.EmailCandidat);
        var recruteur = _recruteurRepository.FindByEmail(createOfferRequest.EmailRecruteur);
        var result = _planifierEntretien.Execute(candidat, createOfferRequest.DisponibiliteCandidat,
            recruteur, createOfferRequest.DisponibiliteRecruteur);
        if (result)
        {
            return Task.FromResult<IActionResult>(CreatedAtAction("Create", new { id = createOfferRequest }, createOfferRequest));
        }
        return Task.FromResult<IActionResult>(BadRequest());
    }
}