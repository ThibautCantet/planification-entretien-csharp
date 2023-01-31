# Domain event

### Etape 5.1 : application service

Renommer le package `use_case` en `application_service`.

### Etape 5.2 : value object `CandidatId`

Extraire un `value object` `CandidatId` dans le sous-domaine `candidat`.

### Etape 5.3 : methode `Next()` dans `ICandidatRepository`

Modifier le mapping hibernate pour que l'`id` ne soit plus généré par la base de données mais fourni avant de
sauvegarder l'entité.

Ajouter une methode `Integer Next()` dans `ICandidatRepository` qui retourne l'id du futur candidat à créer et qui sera à
appeler avant l'instanciation d'un nouveau candidat.

### Etape 5.4 : ajout de domain event

Ajouter une interface `Event` dans à la racine de `domain` :

````java
public interface Event
{
}
````

Ajouter 2 classes `CandidatCrée` et `CandidatNonCrée` pour gérer le cas passant et les cas d'erreur lors de la création
de `candidat`.

```java
public record CandidatCrée(int value) : Event
{
}

public record CandidatNonCrée() : Event
{
}
```

### Etape 5.5 : ajout d'une factory pour créer les candidats

Ajouter une classe `Result` dans le sous-domaine `candidat` pour encapsuler le domaine event généré par la `factory` et
la nouvelle instance créée.

```java
public record Result<T>(Event Event, T Value);
```

### Etape 5.6 : `CandidatFactory`

Créer une classe `CandidatFactory` qui encapsule les invariants et retourne une instance de `Result`.

```java
public class CandidatFactory
{
    public Result<Candidat> Create(int candidatId, String language, String email, int? experienceEnAnnees) {
        //TODO
    }
}
```

### Etape 5.7 : remonter plutôt de `Event`

Changer la signature de `CreerCandidat` pour que la méthode `Execute` retourne une liste d'`Event` générés par le use
case :

```java
public IEnumerable<Event> Execute(string language, string email, int? experienceEnAnnees)
```

### Etape 5.8 : sous-domaines

Regrouper les use cases dans le package `application_service`.

### Question

Qu'est-ce que cela change que le use case retourne une liste d'`Event` ?