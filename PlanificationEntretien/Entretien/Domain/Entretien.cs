using System;
using PlanificationEntretien.Common.Domain;

namespace PlanificationEntretien.Entretien.Domain;

public interface IEntretien
{
    Candidat Candidat { get; }
    Recruteur Recruteur { get; }
    DateTime Horaire { get; }
    Status Status { get; }
}

public class Entretien : IEquatable<Entretien>, IEntretien
{
    public int Id { get; }
    public Candidat Candidat { get; }
    public Recruteur Recruteur { get; }
    public DateTime Horaire { get; private set; }

    public Status Status { get; private set; }

    private Entretien(int id, Candidat candidat, Recruteur recruteur, DateTime horaire, Status status)
    {
        Id = id;
        Candidat = candidat;
        Recruteur = recruteur;
        Horaire = horaire;
        Status = status;
    }

    public Entretien(int id, Candidat candidat, Recruteur recruteur) : this(id, candidat, recruteur, DateTime.MinValue, Status.NON_PLANIFIE)
    {
    }

    public Event Planifier(DateTime disponibiliteDuCandidat, DateTime disponibiliteDuRecruteur)
    {
        var planifiable = Recruteur.EstCompatible(Candidat) && disponibiliteDuCandidat.Equals(disponibiliteDuRecruteur);
        if (planifiable)
        {
            Horaire = disponibiliteDuCandidat;
            return new EntretienCréé(Id, Recruteur.Id);
        }
        return new EntretienNonCréé();
    }

    public void Valider()
    {
        Status = Status.VALIDEE;
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

    public static Entretien of(int id, Candidat candidat, Recruteur recruteur, DateTime horaire, Status status)
    {
        return new Entretien(id, candidat, recruteur, horaire, status);
    }
}