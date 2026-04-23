using PlanificationEntretien.candidat.application_service;
using PlanificationEntretien.candidat.domain;
using PlanificationEntretien.candidat.infrastructure.repository;
using PlanificationEntretien.entretien.application_service;
using PlanificationEntretien.entretien.domain;
using PlanificationEntretien.entretien.infrastructure.repository;
using PlanificationEntretien.infrastructure.email;
using PlanificationEntretien.recruteur.application_service;
using PlanificationEntretien.recruteur.domain;
using PlanificationEntretien.recruteur.infrastructure.repository;
using Candidat = PlanificationEntretien.candidat.domain.Candidat;
using Recruteur = PlanificationEntretien.recruteur.domain.Recruteur;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddSingleton<ICandidatRepository, InMemoryCandidatRepository>();
builder.Services.AddSingleton<IRecruteurRepository, InMemoryRecruteurRepository>();
builder.Services.AddSingleton<IEntretienRepository, InMemoryEntretienRepository>();

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
            var recruiter1Id = recruteurRepo.Save(new Recruteur("Java", "java-recruiter@example.com", 10));
            var recruiter2Id = recruteurRepo.Save(new Recruteur("CSharp", "csharp-recruiter@example.com", 8));
            var recruiter3Id = recruteurRepo.Save(new Recruteur("Python", "python-recruiter@example.com", 12));

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
