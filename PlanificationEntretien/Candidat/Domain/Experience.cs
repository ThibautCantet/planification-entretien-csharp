using System;

namespace PlanificationEntretien.Candidat.Domain;

public record Experience() {
    public Experience(int? annee) : this() {
        if (!annee.HasValue || annee <= 0) {
            throw new ArgumentException();
        }
        Annee = annee.Value;
    }

    public int Annee { get; }
}