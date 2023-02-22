using System.Collections.Generic;
using PlanificationEntretien.domain;
using PlanificationEntretien.candidat.domain_service;
using PlanificationEntretien.candidat.domain;

namespace PlanificationEntretien.candidat.application_service;

public class CreerCandidatCommandHandler
{
    private readonly ICandidatRepository _candidatRepository;
    private readonly CandidatFactory _candidatFactory;

    public CreerCandidatCommandHandler(ICandidatRepository candidatRepository, CandidatFactory candidatFactory)
    {
        _candidatRepository = candidatRepository;
        _candidatFactory = candidatFactory;
    }

    public IEnumerable<Event> Handle(string language, string email, int? experienceEnAnnees)
    {
        var candidatId = _candidatRepository.Next();
        var eventCandidatResult = _candidatFactory.Create(candidatId, language, email, experienceEnAnnees);

        var candidatCrée = eventCandidatResult.Event as CandidatCrée;
        if (candidatCrée != null)
        {
            _candidatRepository.Save(eventCandidatResult.Value);
        }

        var events = new List<Event>();
        events.Add(eventCandidatResult.Event);
        
        return events;
    }
}