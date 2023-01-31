using System;
using System.Collections.Generic;
using Planification_Entretien.domain;
using Planification_Entretien.domain_service.candidat;
using PlanificationEntretien.domain.candidat;

namespace PlanificationEntretien.application_service.candidat;

public class CreerCandidat
{
    private readonly ICandidatRepository _candidatRepository;
    private readonly CandidatFactory _candidatFactory;

    public CreerCandidat(ICandidatRepository candidatRepository, CandidatFactory candidatFactory)
    {
        _candidatRepository = candidatRepository;
        _candidatFactory = candidatFactory;
    }

    public IEnumerable<Event> Execute(string language, string email, int? experienceEnAnnees)
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