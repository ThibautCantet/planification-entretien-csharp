using System;
using Planification_Entretien.domain;
using PlanificationEntretien.domain.candidat;

namespace Planification_Entretien.domain_service.candidat;

public class CandidatFactory
{
    public Result<Candidat> Create(int candidatId, String language, String email, int? experienceEnAnnees) {
        Event e;
        try {
            var candidat = new Candidat(candidatId, language, email, experienceEnAnnees);
            e = new CandidatCrée(candidatId);

            return new Result<Candidat>(e, candidat);
        } catch (ArgumentException ex) {
            e = new CandidatNonCrée();
            return new Result<Candidat>(e, null);
        }
    }
}