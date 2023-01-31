# Message bus et Listener

### Etape 6.1 : tuyauterie pour faire transiter les `Event`

Rajouter les classes `Listener` et `MessageBus` dans le package `application_service` :

```C#
public interface Listener
{
    void OnMessage(E msg);
}

public class MessageBus
{
    private List<Listener> subs = new();

    public void Subscribe(Listener l) {
        subs.Add(l);
    }

    public void Send(Event msg) {
        foreach (var l in subs) {
            l.OnMessage(msg);
        }
    }
}
```

### Etape 6.2 : Publication d'un domain event

Créer un nouveau `Event` `EntretienCréé` :

```C#
public record EntretienCréé(int EntretienId, int RecruteurId) : Event;
```

Dans le use case `PlanifierEntretien`, ajouter en dépendance un `MessageBus` et publier un `Event` `EntretienCréé` :

```C#
_messageBus.Send(new EntretienCréé(entretien.Id, recruteur.Id));
```

### Etape 6.3 : Ajout d'une propriété disponible pour un recruteur

Rajouter une propriété `disponible` pour un recruteur qui sera modifiée via une nouvelle méthode.

### Etape 6.4 : Interception de l'`Event` `EntretienCréé` et mise à jour du recruteur

Créer un `Listener` `EntretienCreeListener` dans le package `application_service.recruteur` qui va s'occuper de rendre
indisponible le recruteur :

```C#
public class EntretienCréeListener : Listener
{
    private readonly IRecruteurRepository _recruteurRepository;
    private readonly MessageBus _messageBus;

    public EntretienCréeListener(IRecruteurRepository recruteurRepository, MessageBus messageBus) {
        _recruteurRepository = recruteurRepository;
        _messageBus = messageBus;
        _messageBus.Subscribe(this);
    }

    public void OnMessage(Event entretienCréé)
    {
        //TODO
    }
}
```

### Question

Quels sont les intérêts de passer par des domain events ?

### Réponse

- Découplage
- Open Close Principle
