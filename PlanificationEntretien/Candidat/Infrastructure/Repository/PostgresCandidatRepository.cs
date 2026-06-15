using System.Data;
using Dapper;
using Npgsql;
using PlanificationEntretien.Candidat.Domain;

namespace PlanificationEntretien.Candidat.Infrastructure.Repository;

public class PostgresCandidatRepository : ICandidatRepository
{
    private readonly string _connectionString;

    public PostgresCandidatRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);

    public Domain.Candidat FindById(int id)
    {
        using var conn = CreateConnection();
        var row = conn.QueryFirstOrDefault(
            "SELECT id, language, email, experience_en_annees FROM candidat WHERE id = @Id",
            new { Id = id });
        return row == null ? null : ToCandidat(row);
    }

    public Domain.Candidat FindByEmail(string email)
    {
        using var conn = CreateConnection();
        var row = conn.QueryFirstOrDefault(
            "SELECT id, language, email, experience_en_annees FROM candidat WHERE email = @Email",
            new { Email = email });
        return row == null ? null : ToCandidat(row);
    }

    public int Save(Domain.Candidat candidat)
    {
        using var conn = CreateConnection();
        conn.Execute(
            @"INSERT INTO candidat (id, language, email, experience_en_annees)
              VALUES (@Id, @Language, @Email, @ExperienceEnAnnees)
              ON CONFLICT (id) DO UPDATE
              SET language = EXCLUDED.language,
                  email = EXCLUDED.email,
                  experience_en_annees = EXCLUDED.experience_en_annees",
            new
            {
                candidat.Id,
                candidat.Language,
                candidat.Email,
                candidat.ExperienceEnAnnees
            });
        return candidat.Id;
    }

    public int Next()
    {
        using var conn = CreateConnection();
        return conn.ExecuteScalar<int>("SELECT COALESCE(MAX(id), 0) + 1 FROM candidat");
    }

    private static Domain.Candidat ToCandidat(dynamic row)
    {
        return new Domain.Candidat(
            (int)row.id,
            (string)row.language,
            (string)row.email,
            (int)row.experience_en_annees);
    }
}

