using System;

namespace PlanificationEntretien.domain;

public interface IEntretien
{
    Candidat Candidat { get; }
    Recruteur Recruteur { get; }
    DateTime Horaire { get; }
}

public class Entretien : IEquatable<Entretien>, IEntretien
{
    public Candidat Candidat { get; }
    public Recruteur Recruteur { get; }
    public DateTime Horaire { get; private set; }

    private Entretien(Candidat candidat, Recruteur recruteur, DateTime horaire)
    {
        Candidat = candidat;
        Recruteur = recruteur;
        Horaire = horaire;
    }

    public Entretien(Candidat candidat, Recruteur recruteur) : this(candidat, recruteur, DateTime.MinValue)
    {
    }

    public bool Planifier(DateTime disponibiliteDuCandidat, DateTime disponibiliteDuRecruteur)
    {
        var planifiable = Candidat.Language.Equals(Recruteur.Language)
                          && Candidat.ExperienceEnAnnees < Recruteur.ExperienceEnAnnees
                          && disponibiliteDuCandidat.Equals(disponibiliteDuRecruteur);
        if (planifiable)
        {
            Horaire = disponibiliteDuCandidat;
        }
        return planifiable;
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