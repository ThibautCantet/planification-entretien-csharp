# Planification Entretien (C# / .NET)

Application de planification d'entretiens (candidat/recruteur) avec une API REST ASP.NET Core et des tests BDD SpecFlow.

## Prerequis

- macOS / Linux / Windows
- SDK .NET 10 (`dotnet --version`)
- IDE recommande: Rider ou VS Code
- (Optionnel) plugin Rider `SpecFlow for Rider` pour executer les `.feature` directement

## Structure utile

- `PlanificationEntretien/`: domaine + use cases + controllers + repositories in-memory
- `PlanificationEntretien.API/`: host ASP.NET Core (`Program.cs`)
- `PlanificationEntretien.Tests/`: tests BDD SpecFlow/xUnit
- `PlanificationEntretien.sln`: solution principale (inclut API)

## Installation / build

```bash
dotnet restore PlanificationEntretien.sln
dotnet build PlanificationEntretien.sln
```

## Executer les tests

### Tests BDD (recommande)

```bash
dotnet test PlanificationEntretien.Tests/PlanificationEntretien.Tests.csproj --framework net10.0
```

### Tous les tests de la solution

```bash
dotnet test PlanificationEntretien.sln
```

## Demarrer l'API REST

```bash
dotnet run --project PlanificationEntretien.API/PlanificationEntretien.API.csproj
```

Par defaut (profil `Development`), l'API expose:

- `http://localhost:5099`
- `https://localhost:7099`
- Swagger UI: `http://localhost:5099/swagger` (ou en HTTPS)

Si le port HTTPS est deja pris, vous pouvez lancer en HTTP uniquement:

```bash
dotnet run --project PlanificationEntretien.API/PlanificationEntretien.API.csproj --urls "http://localhost:5099"
```

## Endpoints REST principaux

- `POST /api/candidat`
- `POST /api/recruteur`
- `GET /api/recruteur`
- `POST /api/entretien`
- `GET /api/entretien`

Exemple `POST /api/entretien`:

```bash
curl -X POST "http://localhost:5099/api/entretien" \
  -H "Content-Type: application/json" \
  -d '{
    "idCandidat": 1,
    "idRecruteur": 1,
    "disponibiliteCandidat": "2026-04-23T15:00:00",
    "disponibiliteRecruteur": "2026-04-23T15:00:00"
  }'
```

## Notes pratiques

- Les repositories sont in-memory: redemarrage de l'API = donnees perdues.
- `Program.cs` injecte un jeu de donnees de demo au demarrage.
- Les regles metier de planification sont centralisees dans le use case `PlanifierEntretien` (langage, experience, disponibilite).

