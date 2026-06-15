using Dapper;
using Npgsql;
using TechTalk.SpecFlow;

namespace PlanificationEntretien.Hooks
{
    [Binding]
    public class Hooks
    {
        private const string ConnectionString =
            "Host=localhost;Port=5432;Database=planification_entretien;Username=postgres;Password=postgres";

        [BeforeScenario(Order = 0)]
        public void CleanDatabase()
        {
            using var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            // TRUNCATE CASCADE gère automatiquement les FK dans le bon ordre
            conn.Execute("TRUNCATE TABLE entretien, candidat, recruteur RESTART IDENTITY CASCADE");
        }
    }
}