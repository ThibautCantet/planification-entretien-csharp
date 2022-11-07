namespace PlanificationEntretien.domain
{
    public class Candidat
    {
        public string Language { get; }
        public string Email { get; }
        public int ExperienceEnAnnees { get; }

        public Candidat(string language, string email, int? experienceEnAnnees)
        {
            Language = language;
            Email = email;
            ExperienceEnAnnees = experienceEnAnnees.GetValueOrDefault(-1);
        }
    }
}