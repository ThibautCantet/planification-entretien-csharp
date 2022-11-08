using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.service;

namespace PlanificationEntretien.controller;

[ApiController]
[Route("/api/entretien")]
public class EntretienController : ControllerBase
{
    private readonly EntretienService _entretienService;

    public EntretienController(EntretienService entretienService)
    {
        _entretienService = entretienService;
    }

    
    [HttpPost("")]
    public Task<IActionResult> Create([FromBody] CreateEntretienRequest createOfferRequest)
    {
        var result = _entretienService.Planifier(createOfferRequest.EmailCandidat, createOfferRequest.DisponibiliteCandidat,
            createOfferRequest.EmailRecruteur, createOfferRequest.DisponibiliteRecruteur);
        if (result)
        {
            return Task.FromResult<IActionResult>(CreatedAtAction("Create", new { id = createOfferRequest }, createOfferRequest));
        }
        else
        {
            return Task.FromResult<IActionResult>(BadRequest());
        }
    }
}