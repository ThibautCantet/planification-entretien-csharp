using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.domain.entretien;
using PlanificationEntretien.domain.recruteur;

namespace PlanificationEntretien.infrastructure.repository;

public class InMemoryRecruteurRepository : IRecruteurRepository , IRecruteurAssigneDao
{
    private Dictionary<string, InMemoryRecruteur> _recruteurs = new();

    public InMemoryRecruteurRepository()
    {
    }

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
        var newId = _recruteurs.Count + 1;
        _recruteurs.TryAdd(recruteur.Email, ToInMemoryRecruteur(recruteur, newId));

        return newId;
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

    internal static RecruteurAssigné ToRecruteurAssigné(InMemoryRecruteur? value)
    {
        return new RecruteurAssigné(value.Id, value.Language, value.Email, value.ExperienceEnAnnees.Value);
    }

    internal static InMemoryRecruteur ToInMemoryRecruteur(Recruteur recruteur, int idRecruteur)
    {
        return new InMemoryRecruteur(idRecruteur, recruteur.Language, recruteur.Email, recruteur.ExperienceEnAnnees);
    }

    internal static InMemoryRecruteur ToInMemoryRecruteur(RecruteurAssigné recruteur)
    {
        return new InMemoryRecruteur(recruteur.Id, recruteur.Profil.Language, recruteur.Email, recruteur.Profil.AnnéeExperience);
    }

    RecruteurAssigné IRecruteurAssigneDao.FindById(int id)
    {
        var recruteur = _recruteurs.Values.FirstOrDefault(r => r.Id == id);
        if (recruteur == null)
        {
            return null;
        }
        return ToRecruteurAssigné(recruteur);
    }
}