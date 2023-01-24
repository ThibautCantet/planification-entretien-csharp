namespace PlanificationEntretien.domain.entretien;

public record Profil(string Language, int ExperienceEnAnnees) {

    public bool EstCompatible(Profil candidat) {
        return Language.Equals(candidat.Language)
               && ExperienceEnAnnees > candidat.ExperienceEnAnnees;
    }
}