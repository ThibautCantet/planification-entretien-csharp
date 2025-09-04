
namespace PlanificationEntretien.domain.recruteur;

public class Profil
{
    public int ExperienceEnAnnees { get; private set; }

    public string Langage { get; private set; }

    public Profil(string langage, int? experienceEnAnnees)
    {
        Langage = new Langage(langage).Value;
        ExperienceEnAnnees = new ExperienceEnAnnees(experienceEnAnnees).Value;
    }
}