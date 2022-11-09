using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.domain;
using PlanificationEntretien.infrastructure.memory;

namespace PlanificationEntretien.application;

[ApiController]
[Route("/api/candidat")]
public class CandidatController : ControllerBase
{
    private readonly ICandidatPort _candidatPort;

    public CandidatController(ICandidatPort candidatPort)
    {
        _candidatPort = candidatPort;
    }

    [HttpPost("")]
    public Task<IActionResult> Create([FromBody] CreateCandidatRequest createCandidatRequest)
    {
        var candidat = new Candidat(createCandidatRequest.Language,
            createCandidatRequest.Email,
            createCandidatRequest.XP);
        if (!string.IsNullOrEmpty(candidat.Email) && IsValid(candidat.Email)
                                                  && !string.IsNullOrEmpty(candidat.Language)
                                                  && candidat.ExperienceEnAnnees > 0)
        {
            _candidatPort.Save(candidat);
            return Task.FromResult<IActionResult>(CreatedAtAction("Create", new { id = createCandidatRequest },
                createCandidatRequest));
        }
        return Task.FromResult<IActionResult>(BadRequest());
    }

    private static bool IsValid(string email)
    {
        try
        {
            new MailAddress(email);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}