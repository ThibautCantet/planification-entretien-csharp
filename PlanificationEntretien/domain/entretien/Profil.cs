using System;

namespace PlanificationEntretien.domain.entretien;

public record Profil(String Language, int AnnéeExperience)
{
    public bool EstCompatible(Profil profil)
    {
        return Language.Equals(profil.Language) && AnnéeExperience > profil.AnnéeExperience;
    } 
};