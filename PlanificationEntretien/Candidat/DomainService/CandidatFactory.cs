using System;
using PlanificationEntretien.Candidat.Domain;
using PlanificationEntretien.Common.domain_service;
using PlanificationEntretien.Common.Domain;

namespace PlanificationEntretien.Candidat.DomainService;

public class CandidatFactory
{
    public Result<Domain.Candidat> Create(int candidatId, String language, String email, int? experienceEnAnnees) {
        Event e;
        try {
            var candidat = new Domain.Candidat(candidatId, language, email, experienceEnAnnees);
            e = new CandidatCrée(candidatId);

            return new Result<Domain.Candidat>(e, candidat);
        } catch (ArgumentException ex) {
            e = new CandidatNonCrée();
            return new Result<Domain.Candidat>(e, null);
        }
    }
}