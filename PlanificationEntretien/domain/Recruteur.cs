using System;
using System.Net.Mail;

namespace PlanificationEntretien.domain;

public class Recruteur : IEquatable<Recruteur>
{
    private readonly int MINIMUM_XP_REQUISE = 2;
    public string Language { get; }
    public string Email { get; }
    public int ExperienceEnAnnees { get; }

    public Recruteur(string language, string email, int? experienceEnAnnees)
    {
        if (string.IsNullOrEmpty(email) || !IsValid(email)
                                        || !email.EndsWith("soat.fr")
                                        || string.IsNullOrEmpty(language)
                                        || experienceEnAnnees == null
                                        || experienceEnAnnees <= MINIMUM_XP_REQUISE)
        {
            throw new ArgumentException();
        }

        Language = language;
        Email = email;
        ExperienceEnAnnees = experienceEnAnnees.GetValueOrDefault(-1);
    }

    private static bool IsValid(string email)
    {
        try
        {
            new MailAddress(email);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public bool Equals(Recruteur other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Language == other.Language && Email == other.Email && ExperienceEnAnnees == other.ExperienceEnAnnees;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Recruteur)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Language, Email, ExperienceEnAnnees);
    }
}