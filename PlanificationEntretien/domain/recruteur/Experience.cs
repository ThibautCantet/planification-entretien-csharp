using System;

namespace PlanificationEntretien.domain.recruteur;

record Experience() {
    private readonly int MINIMUM_XP_REQUISE = 3;

    public Experience(int? annee) : this() {
        if (!annee.HasValue || annee < MINIMUM_XP_REQUISE) {
            throw new ArgumentException();
        }
        Annee = annee.Value;
    }

    public int Annee { get; }
}