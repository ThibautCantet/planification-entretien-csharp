using System;
using System.Collections.Generic;
using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.domain;
using PlanificationEntretien.candidat.domain_service;
using PlanificationEntretien.candidat.domain;

namespace PlanificationEntretien.candidat.application_service;

public class CreerCandidatCommandHandler : ICommandHandler<CreerCandidatCommand, CommandResponse>
{
    private readonly ICandidatRepository _candidatRepository;
    private readonly CandidatFactory _candidatFactory;

    public CreerCandidatCommandHandler(ICandidatRepository candidatRepository, CandidatFactory candidatFactory)
    {
        _candidatRepository = candidatRepository;
        _candidatFactory = candidatFactory;
    }

    public CommandResponse Handle(CreerCandidatCommand creerCandidatCommand)
    {
        var candidatId = _candidatRepository.Next();
        var eventCandidatResult = _candidatFactory.Create(candidatId, creerCandidatCommand.Language, creerCandidatCommand.Email, creerCandidatCommand.ExperienceEnAnnees);

        var candidatCrée = eventCandidatResult.Event as CandidatCrée;
        if (candidatCrée != null)
        {
            _candidatRepository.Save(eventCandidatResult.Value);
        }

        var events = new List<Event>();
        events.Add(eventCandidatResult.Event);
        
        return new CommandResponse(events);
    }

    public Type ListenTo()
    {
        return typeof(CreerCandidatCommand);
    }

}