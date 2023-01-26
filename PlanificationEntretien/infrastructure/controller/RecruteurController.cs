using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.use_case;

namespace PlanificationEntretien.infrastructure.controller;

[ApiController]
[Route("/api/recruteure")]
public class RecruteurController : ControllerBase
{
    private readonly CreerRecruteur _creerRecruteur;
    private readonly ListerRecruteurExperimente _listerRecruteurExperimente;

    public RecruteurController(CreerRecruteur creerRecruteur, ListerRecruteurExperimente listerRecruteurExperimente)
    {
        _creerRecruteur = creerRecruteur;
        _listerRecruteurExperimente = listerRecruteurExperimente;
    }

    public record RecruteurCreationDto(string email);

    [HttpPost("")]
    public ActionResult Create([FromBody] CreateRecruteurRequest createRecruteurRequest)
    {
        var idRecruteur = _creerRecruteur.Execute(createRecruteurRequest.Language,
            createRecruteurRequest.Email,
            createRecruteurRequest.XP);
        
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
        var recruteurs = _listerRecruteurExperimente.Execute()
            .Select(r => new RecruteurExperimenteResponse(r.Email, r.Language, r.ExperienceEnAnnees))
            .ToList();
        return Task.FromResult<IActionResult>(Ok(recruteurs));
    }
}