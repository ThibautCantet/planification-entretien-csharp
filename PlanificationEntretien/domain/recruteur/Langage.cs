using System;

namespace PlanificationEntretien.domain.recruteur;

public class Langage
{
    public string Value { get; }

    public Langage(string value)
    {
        if (string.IsNullOrEmpty(value) || value == "Python")
        {
            throw new ArgumentException();
        }

        Value = value;
    }
}