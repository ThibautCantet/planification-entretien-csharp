using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.infrastructure.repository;

public class InMemoryRecruteurRepository : IRecruteurRepository
{
    private Dictionary<string, InMemoryRecruteur> _recruteurs = new();

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

    public void Save(Recruteur recruteur)
    {
        var newId = _recruteurs.Count + 1;
        _recruteurs.TryAdd(recruteur.Email, ToInMemoryRecruteur(recruteur, newId));
    }

    public List<Recruteur> FindAll()
    {
        return _recruteurs.Values
            .Select(r => new Recruteur(r.Language, r.Email, r.ExperienceEnAnnees))
            .ToList();
    }

    internal static Recruteur ToRecruteur(InMemoryRecruteur? value)
    {
        return new Recruteur(value.Id, value.Language, value.Email, value.ExperienceEnAnnees);
    }

    internal static InMemoryRecruteur ToInMemoryRecruteur(Recruteur recruteur)
    {
        return new InMemoryRecruteur(recruteur.Id, recruteur.Language, recruteur.Email, recruteur.ExperienceEnAnnees);
    }
    
    internal static InMemoryRecruteur ToInMemoryRecruteur(Recruteur recruteur, int idRecruteur)
    {
        return new InMemoryRecruteur(idRecruteur, recruteur.Language, recruteur.Email, recruteur.ExperienceEnAnnees);
    }
}