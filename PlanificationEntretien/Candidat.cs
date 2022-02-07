namespace PlanificationEntretien
{
    public record Candidat()
    {
        public string Language { get; }
        public string Email { get; }
        public int Xp { get; }

        public Candidat(string language, string email, int xp) : this()
        {
            Language = language;
            Email = email;
            Xp = xp;
        }
    }
}