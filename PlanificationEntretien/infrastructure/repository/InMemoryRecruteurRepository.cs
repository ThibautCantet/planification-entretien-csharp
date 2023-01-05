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
        _recruteurs.TryAdd(recruteur.Email, ToInMemoryRecruteur(recruteur));
    }

    public List<Recruteur> FindAll()
    {
        return _recruteurs.Values
            .Select(r => new Recruteur(r.Language, r.Email, r.ExperienceEnAnnees))
            .ToList();
    }

    internal static Recruteur ToRecruteur(InMemoryRecruteur? value)
    {
        return new Recruteur(value.Language, value.Email, value.ExperienceEnAnnees);
    }

    internal static InMemoryRecruteur ToInMemoryRecruteur(Recruteur recruteur)
    {
        return new InMemoryRecruteur(recruteur.Language, recruteur.Email, recruteur.ExperienceEnAnnees);
    }
}