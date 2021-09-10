namespace PlanificationEntretien.domain
{
    public class Recruteur
    {
        public string Language { get; }
        public string Email { get; }
        public int ExperienceEnAnnees { get; }

        public Recruteur(string language, string email, int experienceEnAnnees)
        {
            Language = language;
            Email = email;
            ExperienceEnAnnees = experienceEnAnnees;
        }
    }
}