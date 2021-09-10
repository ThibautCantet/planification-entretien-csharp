using System;

namespace PlanificationEntretien.domain
{
    public class ResultatPlanificationEntretien : IEquatable<ResultatPlanificationEntretien>
    {
        public Candidat Candidat { get; }
        public Recruteur Recruteur { get; }
        public HoraireEntretien HoraireEntretien { get; }

        public ResultatPlanificationEntretien(Candidat candidat, Recruteur recruteur, HoraireEntretien horaireEntretien)
        {
            Candidat = candidat;
            Recruteur = recruteur;
            HoraireEntretien = horaireEntretien;
        }
        
        public bool Equals(ResultatPlanificationEntretien other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Candidat, other.Candidat) && Equals(Recruteur, other.Recruteur) && Equals(HoraireEntretien, other.HoraireEntretien);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ResultatPlanificationEntretien)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Candidat, Recruteur, HoraireEntretien);
        }
    }
}