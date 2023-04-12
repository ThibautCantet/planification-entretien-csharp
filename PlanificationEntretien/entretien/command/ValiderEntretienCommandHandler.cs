using System;
using System.Collections.Generic;
using com.soat.planification_entretien.common.cqrs.command;
using PlanificationEntretien.domain;
using PlanificationEntretien.entretien.domain;

namespace PlanificationEntretien.entretien.application_service;

public class ValiderEntretienCommandHandler : ICommandHandler<ValiderEntretienCommand, CommandResponse>
{
    private readonly IEntretienRepository _entretienRepository;

    public ValiderEntretienCommandHandler(IEntretienRepository entretienRepository)
    {
        _entretienRepository = entretienRepository;
    }

    public CommandResponse Handle(ValiderEntretienCommand command)
    {
        var entretien = _entretienRepository.FindById(command.Id);
        if (entretien != null)
        {
            entretien.Valider();
            _entretienRepository.Save(entretien);
            //return true;
        }

        //return false;
        return new CommandResponse(new List<Event>());
    }

    public Type ListenTo()
    {
        return typeof(ValiderEntretienCommand);
    }
}