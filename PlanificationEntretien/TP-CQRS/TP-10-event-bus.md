# Event bus

### Etape 11

De nouvelles classes et interfaces ont été ajoutées dans `common.cqrs`

Faire la même chose avec les `event` que ce qui vient d'être fait avec les `command` et les `query`.

### Etape 11.1 classes `xxxListener`

Remplacer l'implémentation de l'interface`Listener` par un héritage de `EventHandlerVoid<Event>` et adapter la
classe `xxxListener`.

### Etape 11.2 `EventBusDispatcher`

Ajouter les classes implémentant `EventHandlerVoid` dans la méthode `GetListeners()` de la
classe `EventBusDispatcher` :

Traiter l'event dans la méthode : `Publish(Event @event)` :
```C#
public ICommand Publish(Event @event)
    ...
    case EventHandlerType.VOID:
        if (handler is RecruteurCréeListener) {
            var recruteurCréeListener = handler as RecruteurCréeListener;
            recruteurCréeListener.Handle(@event as RecruteurCrée);
        }
    break;
    ...
```

Enregistrer les listener dans `GetListeners` :
```C#

private IEnumerable<IEventHandler> GetListeners(Event @event)
    {
        if (@event.GetType() == typeof(RecruteurCrée))
        {
            yield return new RecruteurCréeListener(_recruteurDao);
        }
    }
```

### Etape 11.3
Dans la classe `RecruteurATest` et l'étape `@"les recruteurs existant suivants"`, envoyer une commande 
plutôt qu'utiliser directement un command handler afin que ses événements soient publiés sur l'event bus 

```C#
[Given(@"les recruteurs existant suivants")]
public void GivenLesRecruteursExistants(Table table)
{
    var recruteurs = table.Rows.Select(row => BuildRecruteur(row));
    foreach (var recruteur in recruteurs)
    {
        CommandBusFactory().Build().Dispatch(new CreerRecruteurCommand(recruteur.Language, recruteur.Email,
            recruteur.ExperienceEnAnnees));
    }
}
```

### Etape 11.4

Supprimer les interfaces et classes non utilisées `Listener` et `MessageBus`.
