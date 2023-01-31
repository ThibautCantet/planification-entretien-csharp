using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.application_service.candidat;

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
    public ActionResult Create([FromBody] CreateCandidatRequest createCandidatRequest)
    {
        var newId = _creerCandidat.Execute(createCandidatRequest.Language,
            createCandidatRequest.Email,
            createCandidatRequest.Xp);
        if (newId > 0)
        {
            var response = new CreateCandidatResponse(newId,
                createCandidatRequest.Language,
                createCandidatRequest.Email,
                createCandidatRequest.Xp.Value);
            return CreatedAtAction("Create", new { id = createCandidatRequest },
                response);
        }
        return BadRequest();
    }
}