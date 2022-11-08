using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.use_case;

namespace PlanificationEntretien.application;

[ApiController]
[Route("/api/entretien")]
public class EntretienController : ControllerBase
{
    private readonly PlanifierEntretien _planifierEntretien;

    public EntretienController(PlanifierEntretien planifierEntretien)
    {
        _planifierEntretien = planifierEntretien;
    }

    
    [HttpPost("")]
    public Task<IActionResult> Create([FromBody] CreateEntretienRequest createOfferRequest)
    {
        var result = _planifierEntretien.Execute(createOfferRequest.EmailCandidat, createOfferRequest.DisponibiliteCandidat,
            createOfferRequest.EmailRecruteur, createOfferRequest.DisponibiliteRecruteur);
        if (result)
        {
            return Task.FromResult<IActionResult>(CreatedAtAction("Create", new { id = createOfferRequest }, createOfferRequest));
        }
        return Task.FromResult<IActionResult>(BadRequest());
    }
}