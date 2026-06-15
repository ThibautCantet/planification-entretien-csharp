using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Npgsql;
using PlanificationEntretien.Entretien.Domain;

namespace PlanificationEntretien.entretien.Infrastructure.Repository;

public class PostgresEntretienRepository : IEntretienRepository
{
    private readonly string _connectionString;

    public PostgresEntretienRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);

    private const string SelectJoin = @"
        SELECT e.id,
               e.horaire,
               e.status,
               c.id   AS candidat_id,
               c.language AS candidat_language,
               c.email    AS candidat_email,
               c.experience_en_annees AS candidat_xp,
               r.id   AS recruteur_id,
               r.language AS recruteur_language,
               r.email    AS recruteur_email,
               r.experience_en_annees AS recruteur_xp
        FROM entretien e
        JOIN candidat  c ON c.id = e.candidat_id
        JOIN recruteur r ON r.id = e.recruteur_id";

    public Entretien.Domain.Entretien FindById(int id)
    {
        using var conn = CreateConnection();
        var row = conn.QueryFirstOrDefault(SelectJoin + " WHERE e.id = @Id", new { Id = id });
        return row == null ? null : ToEntretien(row);
    }

    public Entretien.Domain.Entretien FindByCandidat(string candidatEmail)
    {
        using var conn = CreateConnection();
        var row = conn.QueryFirstOrDefault(SelectJoin + " WHERE c.email = @Email", new { Email = candidatEmail });
        return row == null ? null : ToEntretien(row);
    }

    public int Save(Entretien.Domain.Entretien entretien)
    {
        using var conn = CreateConnection();
        conn.Execute(
            @"INSERT INTO entretien (id, candidat_id, recruteur_id, horaire, status)
              VALUES (@Id, @CandidatId, @RecruteurId, @Horaire, @Status)
              ON CONFLICT (id) DO UPDATE
              SET horaire = EXCLUDED.horaire,
                  status = EXCLUDED.status",
            new
            {
                entretien.Id,
                CandidatId = entretien.Candidat.Id,
                RecruteurId = entretien.Recruteur.Id,
                entretien.Horaire,
                Status = (int)entretien.Status
            });
        return entretien.Id;
    }

    public IEnumerable<Entretien.Domain.Entretien> FindAll()
    {
        using var conn = CreateConnection();
        var rows = conn.Query(SelectJoin);
        var result = new List<Entretien.Domain.Entretien>();
        foreach (var row in rows)
            result.Add(ToEntretien(row));
        return result;
    }

    public int Next()
    {
        using var conn = CreateConnection();
        return conn.ExecuteScalar<int>("SELECT COALESCE(MAX(id), 0) + 1 FROM entretien");
    }

    private static Entretien.Domain.Entretien ToEntretien(dynamic row)
    {
        var candidat = new Entretien.Domain.Candidat(
            (int)row.candidat_id,
            (string)row.candidat_language,
            (string)row.candidat_email,
            (int)row.candidat_xp);

        var recruteur = new Entretien.Domain.Recruteur(
            (int)row.recruteur_id,
            (string)row.recruteur_language,
            (string)row.recruteur_email,
            (int)row.recruteur_xp);

        return Entretien.Domain.Entretien.of(
            (int)row.id,
            candidat,
            recruteur,
            (DateTime)row.horaire,
            (Status)(int)row.status);
    }
}

