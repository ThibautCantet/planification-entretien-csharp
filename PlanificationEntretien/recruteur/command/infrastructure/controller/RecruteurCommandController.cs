using com.soat.planification_entretien.common.cqrs.application;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.common.cqrs.middleware.command;
using PlanificationEntretien.recruteur.application_service;
using PlanificationEntretien.recruteur.domain;

namespace PlanificationEntretien.recruteur.infrastructure.controller;

[ApiController]
[Route("/api/recruteure")]
public class RecruteurCommandController : CommandController
{

    public RecruteurCommandController(CommandBusFactory commandBusFactory) : base(commandBusFactory)
    {
        _commandBusFactory.Build();
    }

    [HttpPost("")]
    public ActionResult Create([FromBody] CreateRecruteurRequest createRecruteurRequest)
    {
        var commandResponse = base.GetCommandBus().Dispatch(new CreerRecruteurCommand(
            createRecruteurRequest.Language,
            createRecruteurRequest.Email,
            createRecruteurRequest.XP));

        var recruteurCréé = commandResponse.FindFirst<RecruteurCrée>();
        if (recruteurCréé != null)
        {
            var response = new CreateRecruteurResponse(recruteurCréé.Id, 
                createRecruteurRequest.Language,
                createRecruteurRequest.Email,
                createRecruteurRequest.XP.Value);
            
            return CreatedAtAction("Create", new { id = commandResponse },
                response);
        }
        return BadRequest();
    }

}