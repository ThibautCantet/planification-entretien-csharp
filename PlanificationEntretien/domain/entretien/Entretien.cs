using System;

namespace PlanificationEntretien.domain.entretien;

public interface IEntretien
{
    CandidatEvalué CandidatEvalué { get; }
    RecruteurAssigné RecruteurAssigné { get; }
    DateTime Horaire { get; }

    Status Status { get; }
}

public class Entretien : IEquatable<Entretien>, IEntretien
{
    public int Id { get; }
    public CandidatEvalué CandidatEvalué { get; }
    public RecruteurAssigné RecruteurAssigné { get; }
    public DateTime Horaire { get; private set; }
    
    public Status Status { get; private set; }

    private Entretien(int id, CandidatEvalué candidatEvalué, RecruteurAssigné recruteurAssigné, DateTime horaire, Status status)
    {
        Id = id;
        CandidatEvalué = candidatEvalué;
        RecruteurAssigné = recruteurAssigné;
        Horaire = horaire;
        Status = status;
    }

    public Entretien(CandidatEvalué candidatEvalué, RecruteurAssigné recruteurAssigné) : this(-1, candidatEvalué, recruteurAssigné, DateTime.MinValue, Status.Planifie)
    {
    }

    public bool Planifier(DateTime disponibiliteDuCandidat, DateTime disponibiliteDuRecruteur)
    {
        var planifiable = RecruteurAssigné.EstCompatible(CandidatEvalué)
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
        return Id == other.Id && CandidatEvalué.Equals(other.CandidatEvalué) && RecruteurAssigné.Equals(other.RecruteurAssigné) && Horaire.Equals(other.Horaire) && Status.Equals(other.Status);
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
        return HashCode.Combine(Id, CandidatEvalué, RecruteurAssigné, Horaire, Status);
    }

    public static Entretien of(int id, CandidatEvalué candidatEvalué, RecruteurAssigné recruteurAssigné, DateTime horaire, Status status)
    {
        return new Entretien(id, candidatEvalué, recruteurAssigné, horaire, status);
    }

    public void Valider()
    {
        if (Status == Status.Annule)
            return;
        
        Status = Status.Valide;
    }

    public void Annuler()
    {
        Status = Status.Annule;
    }
}