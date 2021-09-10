using System;

namespace PlanificationEntretien.domain
{
    public class Disponibilite
    {
        public DateTime Horaire { get; }

        public Disponibilite(DateTime horaire)
        {
            Horaire = horaire;
        }

        public bool Verifier(DateTime dateDeDisponibiliteDuRecruteur)
        {
            return Horaire.Date.Equals(dateDeDisponibiliteDuRecruteur);
        }
    }
}