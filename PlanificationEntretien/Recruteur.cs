namespace PlanificationEntretien
{
    public record Recruteur()
    {
        public string Language { get; }
        public string Email { get; }
        public int Xp { get; }

        public Recruteur(string language, string email, int xp) : this()
        {
            Language = language;
            Email = email;
            Xp = xp;
        }
    }
}