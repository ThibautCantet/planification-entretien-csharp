using System;

namespace PlanificationEntretien.domain.candidat;

public class Profile
{
    public int ExperienceEnAnnees { get; private set; }

    public string Langage { get; private set; }

    public Profile(string langage, int? experienceEnAnnees)
    {
        if (string.IsNullOrEmpty(langage)
            || experienceEnAnnees == null
            || experienceEnAnnees <= 0)
        {
            throw new ArgumentException();
        }
        Langage = langage;
        ExperienceEnAnnees = experienceEnAnnees.GetValueOrDefault(-1);
    }
}