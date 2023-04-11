# Projection

### Etape 9

L'idée est d'avoir une projection des recrut.eur.euse.s experimenté.e.s au lieu de faire une requête filtrant par XP et
formattant les informations.

C'est-à-dire avoir stocker exactement ce qu'on veut avoir en retour par la query :

- format de la donnée
- donnée déjà filtrée

#### Etape 9.1

Créer un nouveau modèle de persitance adapté à la query.

Créer une nouvelle une entité `in memory` stockant les informations nécessaires au
queryHandler `ListerRecruteursExperimentesQueryHandler`.

```C#
public record RecruteurDetail(
    int Id,
    string Competence,
    string Email)
{
    public RecruteurDetail(int id, string language, int experienceInYears, string email) : this(id, string.Format("{0} années en {1}", experienceInYears, language), email) {
    }
}
```

Modifier le `InMemoryRecruteurDao` pour stocker en mémoire le nouveau modèle :

```C#
public class InMemoryRecruteurDao : IRecruteurDao
{
    private readonly List<RecruteurDetail> _recruteurs = new();
```

#### Etape 9.2

Créer un événement `RecruteurCrée`

```C#
public record RecruteurCrée(int Id, string Language, int ExperienceInYears, string Email) : Event;
```

#### Etape 9.3

Lever l'event `RecruteurCrée` dans `CreerRecruteurCommandHandler`.

```C#
public int Handle(CreerRecruteurCommand command)
    {
        try
        {
            ...
            _messageBus.Send(new RecruteurCrée(savedRecruteurId,
                command.Language,
                command.ExperienceEnAnnees.Value,
                command.Email
            ));
            ...
```

#### Etape 9.4

Créer un listener `RecruteurCréeListener` dans `recruteur.query.application` :

```C#
public class RecruteurCréeListener : Listener
{
    private readonly IRecruteurDao recruteurDao;
    private readonly MessageBus messageBus;

    public RecruteurCréeListener(IRecruteurDao recruteurDao, MessageBus messageBus) {
        this.recruteurDao = recruteurDao;
        this.messageBus = messageBus;
        this.messageBus.Subscribe(this);
    }

    public void OnMessage(Event e)
    {
        //TODO
            recruteurDao.AddExperimente(new RecruteurDetail(...));
        }
    }
}
```

#### Etape 9.4

Couper la dépendance entre `InMemoryRecruteurDao` et `InMemoryRecruteurRepository`.

Modifier `InMemoryRecruteurDao` pour stocker les objets que l'on veut retourner :

```C#
public class InMemoryRecruteurDao : IRecruteurDao
{
    private readonly List<RecruteurDetail> _recruteurs = new();

    public List<RecruteurDetail> Find10AnsExperience()
    {
        // TODO
    }

    public void AddExperimente(RecruteurDetail recruteur)
    {
        // TODO
    }
}
```

#### Etape 9.5
Pour que le listener soit bien enregistré, il faut modifier l'étape suivante dans la classe `RecruteurATest.cs` 
pour que ce soit un `CreerRecruteurCommandHandler` qui sauvegarder un recruteur et non pas directement le repository
```C#
        public void GivenLesRecruteursExistants(Table table)
        {
            var recruteurs = table.Rows.Select(row => BuildRecruteur(row));
            var entretienCréeListener = new RecruteurCréeListener(RecruteurDao(), _messageBus);
            var creerRecruteurCommandHandler = new CreerRecruteurCommandHandler(RecruteurRepository(), _messageBus);
            foreach (var recruteur in recruteurs)
            {
                creerRecruteurCommandHandler.Handle(new CreerRecruteurCommand(recruteur.Language, recruteur.Email,
                    recruteur.ExperienceEnAnnees));
            }
        }
```

### Question

Quels avantages voyez vous avez les projections ?

### Réponses

On retourne directement ce qu'on souhaite sans avoir à traduire quoi que ce soit.