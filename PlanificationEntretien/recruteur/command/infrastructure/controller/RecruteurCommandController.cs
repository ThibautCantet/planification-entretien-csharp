using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.recruteur.application_service;

namespace PlanificationEntretien.recruteur.infrastructure.controller;

[ApiController]
[Route("/api/recruteure")]
public class RecruteurCommandController : ControllerBase
{
    private readonly CreerRecruteurCommandHandler _creerRecruteurCommandHandler;

    public RecruteurCommandController(CreerRecruteurCommandHandler creerRecruteurCommandHandler)
    {
        _creerRecruteurCommandHandler = creerRecruteurCommandHandler;
    }

    public record RecruteurCreationDto(string email);

    [HttpPost("")]
    public ActionResult Create([FromBody] CreateRecruteurRequest createRecruteurRequest)
    {
        var idRecruteur = _creerRecruteurCommandHandler.Handle(new CreerRecruteurCommand(
            createRecruteurRequest.Language,
            createRecruteurRequest.Email,
            createRecruteurRequest.XP));
        
        if (idRecruteur.Events().Count > 0)
        {
            var response = new CreateRecruteurResponse(
                0, 
                createRecruteurRequest.Language,
                createRecruteurRequest.Email,
                createRecruteurRequest.XP.Value);
            
            return CreatedAtAction("Create", new { id = idRecruteur },
                response);
        }
        return BadRequest();
    }

}