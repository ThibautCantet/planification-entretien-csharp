using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.domain;
using PlanificationEntretien.use_case;

namespace PlanificationEntretien.application;

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
        var candidat = new Candidat(createCandidatRequest.Language,
            createCandidatRequest.Email,
            createCandidatRequest.XP);
        if (_creerCandidat.Execute(candidat)) {
            return Task.FromResult<IActionResult>(CreatedAtAction("Create", new { id = createCandidatRequest },
                createCandidatRequest));
        }
        return Task.FromResult<IActionResult>(BadRequest());
    }
}