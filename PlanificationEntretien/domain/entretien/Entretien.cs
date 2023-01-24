using System;

namespace PlanificationEntretien.domain.entretien;

public interface IEntretien
{
    Candidat Candidat { get; }
    Recruteur Recruteur { get; }
    DateTime Horaire { get; }
}

public class Entretien : IEquatable<Entretien>, IEntretien
{
    public int Id { get; }
    public Candidat Candidat { get; }
    public Recruteur Recruteur { get; }
    public DateTime Horaire { get; private set; }

    private Entretien(int id, Candidat candidat, Recruteur recruteur, DateTime horaire)
    {
        Id = id;
        Candidat = candidat;
        Recruteur = recruteur;
        Horaire = horaire;
    }

    public Entretien(Candidat candidat, Recruteur recruteur) : this(-1, candidat, recruteur, DateTime.MinValue)
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

    public bool Equals(Entretien? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && Candidat.Equals(other.Candidat) && Recruteur.Equals(other.Recruteur) && Horaire.Equals(other.Horaire);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Entretien)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Candidat, Recruteur, Horaire);
    }

    public static Entretien of(int id, Candidat candidat, Recruteur recruteur, DateTime horaire)
    {
        return new Entretien(id, candidat, recruteur, horaire);
    }
}