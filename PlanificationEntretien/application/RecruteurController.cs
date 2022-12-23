using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.domain;
using PlanificationEntretien.use_case;

namespace PlanificationEntretien.application;

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
    public Task<IActionResult> Create([FromBody] CreateRecruteurRequest createRecruteurRequest)
    {
        var recruteur = new Recruteur(createRecruteurRequest.Language,
            createRecruteurRequest.Email,
            createRecruteurRequest.XP);
        if (_creerRecruteur.Execute(recruteur))
        {
            return Task.FromResult<IActionResult>(CreatedAtAction("Create", new { id = createRecruteurRequest },
                createRecruteurRequest));
        }
        return Task.FromResult<IActionResult>(BadRequest());
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