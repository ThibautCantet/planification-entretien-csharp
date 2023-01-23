using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.domain;
using PlanificationEntretien.use_case;

namespace PlanificationEntretien.infrastructure.controller;

[ApiController]
[Route("/api/candidat")]
public class CandidatController : ControllerBase
{
    private readonly CreerCandidat _creerCandidat;

    public CandidatController(CreerCandidat creerCandidat)
    {
        _creerCandidat = creerCandidat;
    }

    [HttpPost("")]
    public Task<IActionResult> Create([FromBody] CreateCandidatRequest createCandidatRequest)
    {
        if (_creerCandidat.Execute(createCandidatRequest.Language,
                createCandidatRequest.Email,
                createCandidatRequest.XP)) {
            return Task.FromResult<IActionResult>(CreatedAtAction("Create", new { id = createCandidatRequest },
                createCandidatRequest));
        }
        return Task.FromResult<IActionResult>(BadRequest());
    }
}