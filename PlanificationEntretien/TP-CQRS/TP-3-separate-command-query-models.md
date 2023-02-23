# Séparation des modèle de Command et Query

### Etape 4

Séparer modèles du domaine pour la création (`Command`) et de lecture (`Query`) et les mettre dans les packages :

`command` :

- `domain`
- `domain_service`

`query` :

- `application`

### Question

Que remarquez-vous dans les `QueryHandler` ?