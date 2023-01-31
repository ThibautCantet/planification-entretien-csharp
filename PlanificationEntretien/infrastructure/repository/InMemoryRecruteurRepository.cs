using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.domain.recruteur;
using entretienRecruteur = PlanificationEntretien.domain.entretien;

namespace PlanificationEntretien.infrastructure.repository;

public class InMemoryRecruteurRepository : IRecruteurRepository
{
    private Dictionary<string, InMemoryRecruteur> _recruteurs = new();

    public Recruteur FindById(int id)
    {
        var recruteur = _recruteurs.Values.FirstOrDefault(r => r.Id == id);
        if (recruteur == null)
        {
            return null;
        }
        return ToRecruteur(recruteur);
    }

    public Recruteur FindByEmail(string email)
    {
        InMemoryRecruteur value;
        _recruteurs.TryGetValue(email, out value);
        if (value == null)
        {
            return null;
        }
        return ToRecruteur(value);
    }

    public int Save(Recruteur recruteur)
    {
        if (recruteur.Id == 0)
        {
            var newId = _recruteurs.Count + 1;
            _recruteurs.TryAdd(recruteur.Email, ToInMemoryRecruteur(recruteur, newId));

            return newId;
        }

        _recruteurs.Remove(recruteur.Email);
        if (_recruteurs.TryAdd(recruteur.Email, ToInMemoryRecruteur(recruteur)))
        {
            return recruteur.Id;
        }

        return -1;
    }

    public List<Recruteur> FindAll()
    {
        return _recruteurs.Values
            .Select(r => new Recruteur(r.Id, r.Language, r.Email, r.ExperienceEnAnnees, r.EstDisponible))
            .ToList();
    }

    internal static Recruteur ToRecruteur(InMemoryRecruteur? value)
    {
        return new Recruteur(value.Id, value.Language, value.Email, value.ExperienceEnAnnees, value.EstDisponible);
    }
    
    internal static entretienRecruteur.Recruteur ToEntretienRecruteur(InMemoryRecruteur? value)
    {
        return new entretienRecruteur.Recruteur(value.Id, value.Language, value.Email, value.ExperienceEnAnnees.Value);
    }

    internal static InMemoryRecruteur ToInMemoryRecruteur(Recruteur recruteur)
    {
        return ToInMemoryRecruteur(recruteur, recruteur.Id);
    }

    internal static InMemoryRecruteur ToInMemoryRecruteur(Recruteur recruteur, int idRecruteur)
    {
        return new InMemoryRecruteur(idRecruteur, recruteur.Language, recruteur.Email, recruteur.ExperienceEnAnnees, recruteur.EstDisponible);
    }

    internal static InMemoryRecruteur ToInMemoryEntretienRecruteur(entretienRecruteur.Recruteur recruteur)
    {
        return new InMemoryRecruteur(recruteur.Id, recruteur.Language, recruteur.Email, recruteur.ExperienceEnAnnees, true);
    }
}