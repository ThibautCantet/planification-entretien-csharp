using System.Linq;
using com.soat.planification_entretien.common.cqrs.application;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.candidat.application_service;
using PlanificationEntretien.candidat.domain;
using PlanificationEntretien.common.cqrs.middleware.command;

namespace PlanificationEntretien.candidat.infrastructure.controller;

[ApiController]
[Route("/api/candidat")]
public class CandidatController : CommandController
{

    public CandidatController(CommandBusFactory commandBusFactory) : base(commandBusFactory)
    {
        _commandBusFactory.Build();
    }

    [HttpPost("")]
    public ActionResult Create([FromBody] CreateCandidatRequest createCandidatRequest)
    {
        var commandResponse = base.GetCommandBus().Dispatch(new CreerCandidatCommand(createCandidatRequest.Language,
            createCandidatRequest.Email,
            createCandidatRequest.Xp));
        if (commandResponse.FindFirst(typeof(CandidatCrée)) == null)
        {
            return BadRequest();
        }

        if (commandResponse.FindFirst(typeof(CandidatNonSauvegardé)) != null)
        {
            return Problem();
        }

        var candidatCrée = commandResponse.FindFirst(typeof(CandidatCrée)) as CandidatCrée;
        var response = new CreateCandidatResponse(candidatCrée.Id,
            createCandidatRequest.Language,
            createCandidatRequest.Email,
            createCandidatRequest.Xp.Value);
        return CreatedAtAction("Create", new { id = createCandidatRequest },
            response);
    }
}