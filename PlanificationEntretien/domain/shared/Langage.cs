using System;

namespace PlanificationEntretien.domain.shared;

public class Langage
{
    public string Value { get; }

    public Langage(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException();
        }

        Value = value;
    }
}