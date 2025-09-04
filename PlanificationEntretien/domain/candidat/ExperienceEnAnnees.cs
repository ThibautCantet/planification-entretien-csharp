using System;

namespace PlanificationEntretien.domain.candidat;

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