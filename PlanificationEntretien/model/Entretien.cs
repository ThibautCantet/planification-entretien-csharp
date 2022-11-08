using System;

namespace PlanificationEntretien.model;

public class Entretien : IEquatable<Entretien>
{
    public Candidat Candidat { get; }
    public Recruteur Recruteur { get; }
    public DateTime Horaire { get; }

    public Entretien(Candidat candidat, Recruteur recruteur, DateTime horaire)
    {
        Candidat = candidat;
        Recruteur = recruteur;
        Horaire = horaire;
    }


    public bool Equals(Entretien other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(Candidat, other.Candidat) && Equals(Recruteur, other.Recruteur) && Equals(Horaire, other.Horaire);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Entretien)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Candidat, Recruteur, Horaire);
    }

    public static Entretien of(Candidat candidat, Recruteur recruteur, DateTime horaire)
    {
        return new Entretien(candidat, recruteur, horaire);
    }
}