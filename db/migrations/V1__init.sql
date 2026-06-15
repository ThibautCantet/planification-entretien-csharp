-- Migration V1 : Création des tables de la planification d'entretien
-- Les IDs sont générés par l'application (pas de SERIAL/GENERATED)

CREATE TABLE IF NOT EXISTS candidat (
    id                   INT          NOT NULL,
    language             VARCHAR(100) NOT NULL,
    email                VARCHAR(255) NOT NULL,
    experience_en_annees INT          NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS recruteur (
    id                   INT          NOT NULL,
    language             VARCHAR(100) NOT NULL,
    email                VARCHAR(255) NOT NULL,
    experience_en_annees INT          NOT NULL,
    est_disponible       BOOLEAN      NOT NULL DEFAULT TRUE,
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS entretien (
    id           INT       NOT NULL,
    candidat_id  INT       NOT NULL,
    recruteur_id INT       NOT NULL,
    horaire      TIMESTAMP NOT NULL,
    status       INT       NOT NULL DEFAULT 0,
    PRIMARY KEY (id),
    FOREIGN KEY (candidat_id)  REFERENCES candidat(id),
    FOREIGN KEY (recruteur_id) REFERENCES recruteur(id)
);

