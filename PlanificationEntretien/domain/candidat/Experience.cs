using System;

namespace PlanificationEntretien.domain.candidat;

public record Experience() {
    public Experience(int? annee) : this() {
        if (!annee.HasValue || annee <= 0) {
            throw new ArgumentException();
        }
        Annee = annee.Value;
    }

    public int Annee { get; }
}