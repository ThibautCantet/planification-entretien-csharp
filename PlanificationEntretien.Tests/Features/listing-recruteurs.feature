# language: fr
Fonctionnalité: Lister les recruteurs chez Soat

    Scénario: Lister les recruteurs expérimentés
        Etant donné les recruteurs existant suivants
          | id | email              | language | xp |
          | 1  | recruteur1@soat.fr | Java     | 9  |
          | 2  | recruteur2@soat.fr | Java     | 10 |
          | 3  | recruteur3@soat.fr | Java     | 11 |
        Quand on liste les recruteurs expérimentés
        Alors on récupères les recruteurs suivants
          | id | email              | details           |
          | 2  | recruteur2@soat.fr | 10 années en Java |
          | 3  | recruteur3@soat.fr | 11 années en Java |