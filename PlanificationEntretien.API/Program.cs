using PlanificationEntretien.Candidat.ApplicationService;
using PlanificationEntretien.Candidat.Domain;
using PlanificationEntretien.Candidat.Infrastructure.Repository;
using PlanificationEntretien.Entretien.ApplicationService;
using PlanificationEntretien.Entretien.Domain;
using PlanificationEntretien.Entretien.Infrastructure.Email;
using PlanificationEntretien.entretien.Infrastructure.Repository;
using PlanificationEntretien.Recruteur.ApplicationService;
using PlanificationEntretien.Recruteur.Domain;
using PlanificationEntretien.Recruteur.Infrastructure.Repository;
using Candidat = PlanificationEntretien.Candidat.Domain.Candidat;
using Recruteur = PlanificationEntretien.Recruteur.Domain.Recruteur;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories (Postgres ou InMemory selon la config)
var usePostgres = builder.Configuration.GetValue<bool>("UsePostgres");
var connectionString = builder.Configuration.GetConnectionString("Postgres") ?? string.Empty;

if (usePostgres)
{
    builder.Services.AddSingleton<ICandidatRepository>(_ => new PostgresCandidatRepository(connectionString));
    builder.Services.AddSingleton<IRecruteurRepository>(_ => new PostgresRecruteurRepository(connectionString));
    builder.Services.AddSingleton<IEntretienRepository>(_ => new PostgresEntretienRepository(connectionString));
    Console.WriteLine("🐘 Repositories PostgreSQL activés");
}
else
{
    builder.Services.AddSingleton<ICandidatRepository, InMemoryCandidatRepository>();
    builder.Services.AddSingleton<IRecruteurRepository, InMemoryRecruteurRepository>();
    builder.Services.AddSingleton<IEntretienRepository, InMemoryEntretienRepository>();
    Console.WriteLine("💾 Repositories InMemory activés");
}

// Register email service
builder.Services.AddSingleton<IEmailService, ConsoleEmailService>();

// Register use cases
builder.Services.AddSingleton<CreerCandidat>();
builder.Services.AddSingleton<CreerRecruteur>();
builder.Services.AddSingleton<PlanifierEntretien>();
builder.Services.AddSingleton<ListerEntretien>();
builder.Services.AddSingleton<ListerRecruteurExperimente>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed initial data for demonstration
SeedData(app.Services);

app.Run();

/// <summary>
/// Seed initial data into the repositories
/// </summary>
void SeedData(IServiceProvider services)
{
    using (var scope = services.CreateScope())
    {
        var candidatRepo = scope.ServiceProvider.GetRequiredService<ICandidatRepository>();
        var recruteurRepo = scope.ServiceProvider.GetRequiredService<IRecruteurRepository>();

        try
        {
            // Create sample candidates
            var candidat1Id = candidatRepo.Save(new Candidat(1, "Java", "alice@example.com", 3));
            var candidat2Id = candidatRepo.Save(new Candidat(2, "CSharp", "bob@example.com", 2));
            var candidat3Id = candidatRepo.Save(new Candidat(3, "Python", "charlie@example.com", 5));

            // Create sample recruiters
            var recruiter1Id = recruteurRepo.Save(new Recruteur(1, "Java", "java-recruiter@example.com", 10));
            var recruiter2Id = recruteurRepo.Save(new Recruteur(2,"CSharp", "csharp-recruiter@example.com", 8));
            var recruiter3Id = recruteurRepo.Save(new Recruteur(3, "Python", "python-recruiter@example.com", 12));

            Console.WriteLine("✅ Données de démonstration chargées avec succès!");
            Console.WriteLine($"📝 3 Candidats créés (IDs: {candidat1Id}, {candidat2Id}, {candidat3Id})");
            Console.WriteLine($"👔 3 Recruteurs créés (IDs: {recruiter1Id}, {recruiter2Id}, {recruiter3Id})");
            Console.WriteLine();
            Console.WriteLine("🚀 L'API est prête!");
            Console.WriteLine("📖 Swagger disponible en développement");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Erreur lors du chargement des données: {ex.Message}");
        }
    }
}
