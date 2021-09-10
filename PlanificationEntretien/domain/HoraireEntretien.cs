using System;

namespace PlanificationEntretien.domain
{
    public class HoraireEntretien : IEquatable<HoraireEntretien>
    {
        public DateTime Horaire { get; }

        public HoraireEntretien(DateTime horaire)
        {
            Horaire = horaire;
        }

        public bool Equals(HoraireEntretien other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Horaire.Equals(other.Horaire);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((HoraireEntretien)obj);
        }

        public override int GetHashCode()
        {
            return Horaire.GetHashCode();
        }
    }
}