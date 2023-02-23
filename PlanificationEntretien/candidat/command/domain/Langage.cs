using System;

namespace PlanificationEntretien.candidat.domain;

public record Langage()
{
    public Langage(string nom) : this()
    {
        if (string.IsNullOrEmpty(nom))
        {
            throw new ArgumentException();
        }
        Nom = nom;
    }

    public string Nom { get; }
}