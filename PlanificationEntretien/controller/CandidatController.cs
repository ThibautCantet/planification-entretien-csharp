using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.model;
using PlanificationEntretien.repository;

namespace PlanificationEntretien.controller;

[ApiController]
[Route("/api/candidat")]
public class CandidatController : ControllerBase
{
    private readonly CandidatRepository _candidatRepository;

    public CandidatController(CandidatRepository candidatRepository)
    {
        this._candidatRepository = candidatRepository;
    }

    [HttpPost("")]
    public Task<IActionResult> Create([FromBody] CandidatDto candidatDto)
    {
        var candidat = new Candidat(candidatDto.Language,
            candidatDto.Email,
            candidatDto.XP);
        if (!string.IsNullOrEmpty(candidat.Email) && IsValid(candidat.Email)
                                                  && !string.IsNullOrEmpty(candidat.Language)
                                                  && candidat.ExperienceEnAnnees > 0)
        {
            _candidatRepository.Save(candidat);
            return Task.FromResult<IActionResult>(CreatedAtAction("Create", new { id = candidatDto },
                candidatDto));
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