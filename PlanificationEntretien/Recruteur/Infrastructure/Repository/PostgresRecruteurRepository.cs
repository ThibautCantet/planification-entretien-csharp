using System.Collections.Generic;
using System.Data;
using Dapper;
using Npgsql;
using PlanificationEntretien.Recruteur.Domain;

namespace PlanificationEntretien.Recruteur.Infrastructure.Repository;

public class PostgresRecruteurRepository : IRecruteurRepository
{
    private readonly string _connectionString;

    public PostgresRecruteurRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);

    public Recruteur.Domain.Recruteur FindById(int id)
    {
        using var conn = CreateConnection();
        var row = conn.QueryFirstOrDefault(
            "SELECT id, language, email, experience_en_annees, est_disponible FROM recruteur WHERE id = @Id",
            new { Id = id });
        return row == null ? null : ToRecruteur(row);
    }

    public Recruteur.Domain.Recruteur FindByEmail(string email)
    {
        using var conn = CreateConnection();
        var row = conn.QueryFirstOrDefault(
            "SELECT id, language, email, experience_en_annees, est_disponible FROM recruteur WHERE email = @Email",
            new { Email = email });
        return row == null ? null : ToRecruteur(row);
    }

    public int Save(Recruteur.Domain.Recruteur recruteur)
    {
        using var conn = CreateConnection();
        conn.Execute(
            @"INSERT INTO recruteur (id, language, email, experience_en_annees, est_disponible)
              VALUES (@Id, @Language, @Email, @ExperienceEnAnnees, @EstDisponible)
              ON CONFLICT (id) DO UPDATE
              SET language = EXCLUDED.language,
                  email = EXCLUDED.email,
                  experience_en_annees = EXCLUDED.experience_en_annees,
                  est_disponible = EXCLUDED.est_disponible",
            new
            {
                recruteur.Id,
                recruteur.Language,
                recruteur.Email,
                recruteur.ExperienceEnAnnees,
                recruteur.EstDisponible
            });
        return recruteur.Id;
    }

    public List<Recruteur.Domain.Recruteur> FindAll()
    {
        using var conn = CreateConnection();
        var rows = conn.Query(
            "SELECT id, language, email, experience_en_annees, est_disponible FROM recruteur");
        var result = new List<Recruteur.Domain.Recruteur>();
        foreach (var row in rows)
            result.Add(ToRecruteur(row));
        return result;
    }

    public int Next()
    {
        using var conn = CreateConnection();
        return conn.ExecuteScalar<int>("SELECT COALESCE(MAX(id), 0) + 1 FROM recruteur");
    }

    private static Recruteur.Domain.Recruteur ToRecruteur(dynamic row)
    {
        return new Recruteur.Domain.Recruteur(
            (int)row.id,
            (string)row.language,
            (string)row.email,
            (int)row.experience_en_annees,
            (bool)row.est_disponible);
    }
}

