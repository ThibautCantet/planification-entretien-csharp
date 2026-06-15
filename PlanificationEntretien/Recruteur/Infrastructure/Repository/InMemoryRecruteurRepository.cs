using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.Recruteur.Domain;
using entretienRecruteur = PlanificationEntretien.Entretien.Domain;

namespace PlanificationEntretien.Recruteur.Infrastructure.Repository;

public class InMemoryRecruteurRepository : IRecruteurRepository
{
    private Dictionary<string, InMemoryRecruteur> _recruteurs = new();

    public Recruteur.Domain.Recruteur FindById(int id)
    {
        var recruteur = _recruteurs.Values.FirstOrDefault(r => r.Id == id);
        if (recruteur == null)
        {
            return null;
        }
        return ToRecruteur(recruteur);
    }

    public Recruteur.Domain.Recruteur FindByEmail(string email)
    {
        InMemoryRecruteur value;
        _recruteurs.TryGetValue(email, out value);
        if (value == null)
        {
            return null;
        }
        return ToRecruteur(value);
    }

    public int Save(Recruteur.Domain.Recruteur recruteur)
    {
        _recruteurs.Remove(recruteur.Email);
        _recruteurs.TryAdd(recruteur.Email, ToInMemoryRecruteur(recruteur));
        return recruteur.Id;
    }


    public List<Recruteur.Domain.Recruteur> FindAll()
    {
        return _recruteurs.Values
            .Select(r => new Recruteur.Domain.Recruteur(r.Id, r.Language, r.Email, r.ExperienceEnAnnees, r.EstDisponible))
            .ToList();
    }

    public int Next()
    {
        return _recruteurs.Count + 1;
    }

    internal static Recruteur.Domain.Recruteur ToRecruteur(InMemoryRecruteur? value)
    {
        return new Recruteur.Domain.Recruteur(value.Id, value.Language, value.Email, value.ExperienceEnAnnees, value.EstDisponible);
    }
    
    internal static entretienRecruteur.Recruteur ToEntretienRecruteur(InMemoryRecruteur? value)
    {
        return new entretienRecruteur.Recruteur(value.Id, value.Language, value.Email, value.ExperienceEnAnnees.Value);
    }

    internal static InMemoryRecruteur ToInMemoryRecruteur(Recruteur.Domain.Recruteur recruteur)
    {
        return ToInMemoryRecruteur(recruteur, recruteur.Id);
    }

    internal static InMemoryRecruteur ToInMemoryRecruteur(Recruteur.Domain.Recruteur recruteur, int idRecruteur)
    {
        return new InMemoryRecruteur(idRecruteur, recruteur.Language, recruteur.Email, recruteur.ExperienceEnAnnees, recruteur.EstDisponible);
    }

    internal static InMemoryRecruteur ToInMemoryEntretienRecruteur(entretienRecruteur.Recruteur recruteur)
    {
        return new InMemoryRecruteur(recruteur.Id, recruteur.Language, recruteur.Email, recruteur.ExperienceEnAnnees, true);
    }
}