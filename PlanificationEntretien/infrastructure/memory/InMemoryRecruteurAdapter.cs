using System.Collections.Generic;
using PlanificationEntretien.domain;

namespace PlanificationEntretien.infrastructure.memory;

public class InMemoryRecruteurAdapter : IRecruteurPort
{
    private Dictionary<string, InMemoryRecruteur> _candidats = new();

    public Recruteur FindByEmail(string email)
    {
        InMemoryRecruteur value;
        _candidats.TryGetValue(email, out value);
        if (value == null)
        {
            return null;
        }
        return ToRecruteur(value);
    }

    public void Save(Recruteur candidat)
    {
        _candidats.Add(candidat.Email, ToInMemoryRecruteur(candidat));
    }

    internal static Recruteur ToRecruteur(InMemoryRecruteur? value)
    {
        return new Recruteur(value.Language, value.Email, value.ExperienceEnAnnees);
    }

    internal static InMemoryRecruteur ToInMemoryRecruteur(Recruteur candidat)
    {
        return new InMemoryRecruteur(candidat.Language, candidat.Email, candidat.ExperienceEnAnnees);
    }
}