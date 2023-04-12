# Command bus

### Etape 10

De nouvelles classes et interfaces ont été ajoutées dans `common.cqrs`

### Etape 10.1 `CommandBusFactory`

Ajouter les différents `commandHandler` dans la class `CommandBusDispatcher` et ajouter leur dépendances 

````C#
public CommandResponse Dispatch(ICommand command)
    {
        if (command.GetType() == typeof(xxxCommand))
        {
            return new xxxCommandHandler().Handle(command as xxxCommand);
        }

        throw new UnmatchedCommandHandlerException(command);
    }
````

### Etape 10.2 `CommandHandler`

Etendez les `commandHandler` de `CommandHandler<MyCommand, CommandResponse>` et modifier la méthode `Handle` pour
qu'elle retourne une `CommandResponse` :

```C#
public CommandResponse Handle(MyCommand myCommand)
```

Adapter le corps de la méthode `Handle` pour le `return`.

Implémenter la méthode `ListenTo()` :

```C#
public Type ListenTo()
{
    //TODO
}
```

### Etape 10.3 `Controller`

Faire étendre les `controller` de command de la class `CommandController` et appeler `_commandBusFactory.Build();` dans le constructeur.

Supprimer les `commandHandler` des `controller` et dispatcher les `command` sur le `bus` grâce à la
méthode `GetCommandBus().dispatch(new MyCommand());` de la classe mère `CommandController`.

Adapter le retour de la méthode `base.GetCommandBus().Dispatch` qui retourne désormais une `CommandResponse`.

### Question

Qu'est-ce que cela change de passer par un `command bus` ?

### Réponse

Cela permet de :

- découpler les `controller` des `commandHandler`.
- factoriser toute la gestion des `command` en utilisant un même `commandBus` (logs, events...)
