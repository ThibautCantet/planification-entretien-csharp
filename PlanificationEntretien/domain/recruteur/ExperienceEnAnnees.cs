using System;

namespace PlanificationEntretien.domain.recruteur;

public class ExperienceEnAnnees
{
    private readonly int MINIMUM_XP_REQUISE = 2;
    public int  Value { get; }

    public ExperienceEnAnnees(int? experienceEnAnnees)
    {
        if (experienceEnAnnees == null
            || experienceEnAnnees <= MINIMUM_XP_REQUISE)
        {
            throw new ArgumentException();
        }
        
        Value = experienceEnAnnees.GetValueOrDefault(-1);
    }
}