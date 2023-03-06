using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.recruteur.application_service;

namespace PlanificationEntretien.recruteur.infrastructure.controller;

[ApiController]
[Route("/api/recruteure")]
public class RecruteurController : ControllerBase
{
    private readonly CreerRecruteurCommandHandler _creerRecruteurCommandHandler;
    private readonly ListerRecruteurExperimenteQueryHandler _listerRecruteurExperimenteQueryHandler;

    public RecruteurController(CreerRecruteurCommandHandler creerRecruteurCommandHandler, ListerRecruteurExperimenteQueryHandler listerRecruteurExperimenteQueryHandler)
    {
        _creerRecruteurCommandHandler = creerRecruteurCommandHandler;
        _listerRecruteurExperimenteQueryHandler = listerRecruteurExperimenteQueryHandler;
    }

    public record RecruteurCreationDto(string email);

    [HttpPost("")]
    public ActionResult Create([FromBody] CreateRecruteurRequest createRecruteurRequest)
    {
        var idRecruteur = _creerRecruteurCommandHandler.Handle(new CreerRecruteurCommand(
            createRecruteurRequest.Language,
            createRecruteurRequest.Email,
            createRecruteurRequest.XP));
        
        if (idRecruteur > 0)
        {
            var response = new CreateRecruteurResponse(idRecruteur, 
                createRecruteurRequest.Language,
                createRecruteurRequest.Email,
                createRecruteurRequest.XP.Value);
            
            return CreatedAtAction("Create", new { id = idRecruteur },
                response);
        }
        return BadRequest();
    }

    [HttpGet("")]
    public Task<IActionResult> ListerExperimentes()
    {
        var recruteurs = _listerRecruteurExperimenteQueryHandler.Handle(new ListerRecruteurExperimenteQuery())
            .Select(r => new RecruteurExperimenteResponse(r.Email(), r.Competence()))
            .ToList();
        return Task.FromResult<IActionResult>(Ok(recruteurs));
    }
}