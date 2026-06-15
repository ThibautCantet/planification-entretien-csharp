using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.candidat.ApplicationService;
using PlanificationEntretien.Candidat.ApplicationService;
using PlanificationEntretien.Candidat.Domain;

namespace PlanificationEntretien.Candidat.Infrastructure.Controller;

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
        var events = _creerCandidat.Execute(createCandidatRequest.Language,
            createCandidatRequest.Email,
            createCandidatRequest.Xp);
        if (events.All(evt => evt.GetType() != typeof(CandidatCrée)))
        {
            return BadRequest();
        }

        if (events.Any(evt => evt.GetType() == typeof(CandidatNonSauvegardé)))
        {
            return Problem();
        }

        var candidatCrée = events
            .FirstOrDefault(evt => evt.GetType() == typeof(CandidatCrée)) as CandidatCrée;
        var response = new CreateCandidatResponse(candidatCrée.Id,
            createCandidatRequest.Language,
            createCandidatRequest.Email,
            createCandidatRequest.Xp.Value);
        return CreatedAtAction("Create", new { id = createCandidatRequest },
            response);
    }
}