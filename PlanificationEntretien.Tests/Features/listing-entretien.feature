# language: fr
Fonctionnalité: Lister les entretiens chez Soat

    Scénario: Lister les entretiens déjà planifiés
        Etant donné les recruteurs existants
          | id | email              | language | xp |
          | 1  | recruteur1@soat.fr | Java     | 10 |
          | 2  | recruteur2@soat.fr | C#       | 11 |
        Et les candidats existants
          | id | email              | language | xp |
          | 1  | candidat1@mail.com | Java     | 5  |
          | 2  | candidat2@mail.com | Java     | 7  |
          | 3  | candidat3@mail.com | C#       | 2  |
        Et les entretiens existants
          | id | recruteur          | candidat           | horaire          |
          | 1  | recruteur1@soat.fr | candidat1@mail.com | 16/04/2019 15:00 |
          | 2  | recruteur1@soat.fr | candidat2@mail.com | 17/04/2019 15:00 |
          | 3  | recruteur2@soat.fr | candidat3@mail.com | 17/04/2019 15:00 |
        Quand on liste les tous les entretiens
        Alors on récupères les entretiens suivants
          | id | recruteur          | candidat           | language | horaire          |
          | 1  | recruteur1@soat.fr | candidat1@mail.com | Java     | 16/04/2019 15:00 |
          | 2  | recruteur1@soat.fr | candidat2@mail.com | Java     | 17/04/2019 15:00 |
          | 3  | recruteur2@soat.fr | candidat3@mail.com | C#       | 17/04/2019 15:00 |