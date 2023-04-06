# Projection

### Etape 9

L'idée est d'avoir une projection des recrut.eur.euse.s experimenté.e.s au lieu de faire une requête filtrant par XP et
formattant les informations.

#### Etape 9.1
Créer un event dans le domain `recruteur` :
```C#
public record RecruteurCrée(int Id, string Language, int ExperienceInYears, string Email) : Event;
```
#### Etape 9.2
Créer un listener `` dans `recruteur.query.application` :

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
        //TODO enregistrer dans le DAO directement ce qui sera retourné 
    }
}
```

#### Etape 9.3
Lever l'event `RecruteurCrée` dans `CreerRecruteurCommandHandler`.

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
````C#
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
````

### Question

Quels avantages voyez vous avez les projections ?
