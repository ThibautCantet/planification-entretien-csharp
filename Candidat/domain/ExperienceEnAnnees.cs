
namespace Candidat.domain;

public class ExperienceEnAnnees
{
    public int  Value { get; }

    public ExperienceEnAnnees(int? experienceEnAnnees)
    {
        if (experienceEnAnnees == null
            || experienceEnAnnees <= 0)
        {
            throw new ArgumentException();
        }

        Value = experienceEnAnnees.GetValueOrDefault(-1);
    }
}