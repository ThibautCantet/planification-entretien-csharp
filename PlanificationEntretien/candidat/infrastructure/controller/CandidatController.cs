using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.candidat.application_service;
using PlanificationEntretien.candidat.domain;

namespace PlanificationEntretien.candidat.infrastructure.controller;

[ApiController]
[Route("/api/candidat")]
public class CandidatController : ControllerBase
{
    private readonly CreerCandidatCommandHandler _creerCandidatCommandHandler;

    public CandidatController(CreerCandidatCommandHandler creerCandidatCommandHandler)
    {
        _creerCandidatCommandHandler = creerCandidatCommandHandler;
    }

    [HttpPost("")]
    public ActionResult Create([FromBody] CreateCandidatRequest createCandidatRequest)
    {
        var events = _creerCandidatCommandHandler.Handle(new CreerCandidatCommand(createCandidatRequest.Language,
            createCandidatRequest.Email,
            createCandidatRequest.Xp));
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