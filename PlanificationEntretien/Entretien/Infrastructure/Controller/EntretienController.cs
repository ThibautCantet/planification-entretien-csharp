using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.Candidat.Domain;
using PlanificationEntretien.Recruteur.Domain;
using PlanificationEntretien.Entretien.ApplicationService;
using PlanificationEntretien.Entretien.Domain;
using Recruteur = PlanificationEntretien.Entretien.Domain.Recruteur;

namespace PlanificationEntretien.Entretien.Infrastructure.Controller;

[ApiController]
[Route("/api/entretien")]
public class EntretienController : ControllerBase
{
    private readonly PlanifierEntretien _planifierEntretien;
    private readonly ICandidatRepository _candidatRepository;
    private readonly IRecruteurRepository _recruteurRepository;
    private readonly ListerEntretien _listerEntretien;
    private readonly ValiderEntretien _validerEntretien;

    public EntretienController(PlanifierEntretien planifierEntretien, ListerEntretien listerEntretien, ValiderEntretien validerEntretien,
        ICandidatRepository candidatRepository, IRecruteurRepository recruteurRepository)
    {
        _planifierEntretien = planifierEntretien;
        _candidatRepository = candidatRepository;
        _recruteurRepository = recruteurRepository;
        _listerEntretien = listerEntretien;
        _validerEntretien = validerEntretien;
    }

    [HttpPost]
    public ActionResult Create([FromBody] CreateEntretienRequest createOfferRequest)
    {
        var candidat = _candidatRepository.FindById(createOfferRequest.IdCandidat);
        var recruteur = _recruteurRepository.FindById(createOfferRequest.IdRecruteur);
        var events = _planifierEntretien.Execute(
            new Domain.Candidat(candidat.Id, candidat.Language, candidat.Email, candidat.ExperienceEnAnnees),
            createOfferRequest.DisponibiliteCandidat,
            new Domain.Recruteur(recruteur.Id, recruteur.Language, recruteur.Email, recruteur.ExperienceEnAnnees),
            createOfferRequest.DisponibiliteRecruteur);
        EntretienCréé entretienCrée = events.FirstOrDefault(e => e.GetType().Equals(typeof(EntretienCréé))) as EntretienCréé;
        if (entretienCrée != null)
        {
            var response = new CreateEntretienResponse(entretienCrée.EntretienId, candidat.Email, recruteur.Email,
                createOfferRequest.DisponibiliteCandidat);
            return CreatedAtAction("Create", new {id= createOfferRequest}, response);
        }
        return BadRequest();
    }

    [HttpGet]
    public IActionResult Lister()
    {
        var entretiens = _listerEntretien.Execute()
            .Select(entretien => new EntretienResponse(entretien.Candidat.Email,
                entretien.Recruteur.Email,
                entretien.Horaire,
                entretien.Status))
            .ToList();
        return Ok(entretiens);
    }

    public IActionResult Valider(int id)
    {
        if (_validerEntretien.Execute(id))
        {
            return Ok();
        }

        return null;
    }
}