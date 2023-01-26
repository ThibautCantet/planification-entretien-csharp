using System;
using System.Net.Mail;

namespace PlanificationEntretien.domain;

public class Candidat : IEquatable<Candidat>
{
    public int Id { get; }
    public string Language { get; }
    public string Email { get; }
    public int ExperienceEnAnnees { get; }

    public Candidat(int id, string language, string email, int? experienceEnAnnees)
    {
        if (string.IsNullOrEmpty(email) || !IsValid(email)
                                        || email.EndsWith("soat.fr")
                                        || string.IsNullOrEmpty(language)
                                        || experienceEnAnnees == null
                                        || experienceEnAnnees <= 0)
        {
            throw new ArgumentException();
        }

        Id = id;
        Language = language;
        Email = email;
        ExperienceEnAnnees = experienceEnAnnees.GetValueOrDefault(-1);
    }

    public Candidat(string language, string email, int? experienceEnAnnees) : this(0, language, email, experienceEnAnnees)
    {
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

    public bool Equals(Candidat? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Language == other.Language && Email == other.Email && ExperienceEnAnnees == other.ExperienceEnAnnees;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Candidat)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Language, Email, ExperienceEnAnnees);
    }
}