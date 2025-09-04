using System;

namespace PlanificationEntretien.domain.entretien;

public record Profil(String Language, int AnnéeExperience)
{
    public bool EstCompatible(CandidatEvalué candidat)
    {
        return Language.Equals(candidat.Langage) && AnnéeExperience > candidat.ExperienceEnAnnees;
    } 
};