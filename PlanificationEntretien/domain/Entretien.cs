using System;
using PlanificationEntretien.Tests;

namespace PlanificationEntretien.domain
{
    public class Entretien : IEquatable<Entretien>
    {
        public Candidat Candidat { get; }
        public Recruteur Recruteur { get; }
        public HoraireEntretien Horaire { get; private set; }

        private Entretien(Candidat candidat, Recruteur recruteur, HoraireEntretien horaire)
        {
            Candidat = candidat;
            Recruteur = recruteur;
            Horaire = horaire;
        }

        public Entretien(Candidat candidat, Recruteur recruteur)
        {
            Candidat = candidat;
            Recruteur = recruteur;
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

        public static Entretien of(Candidat candidat, Recruteur recruteur, HoraireEntretien horaire)
        {
            return new Entretien(candidat, recruteur, horaire);
        }

        public ResultatPlanificationEntretien Planifier(Disponibilite disponibiliteDuCandidat, DateTime dateDeDisponibiliteDuRecruteur)
        {
            Horaire = new HoraireEntretien(disponibiliteDuCandidat.Horaire);
            if (disponibiliteDuCandidat.Verifier(dateDeDisponibiliteDuRecruteur)) {
                return new EntretienPlanifie(Candidat, Recruteur, Horaire);
            }

            return new EntretienEchouee(Candidat, Recruteur, Horaire);
        }
    }
}