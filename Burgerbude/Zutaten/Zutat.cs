using Burgerbude.Interfaces;

namespace Burgerbude.Zutaten;

public abstract class Zutat : IZutat
{
    public Zutat(string name, double preis)
    {
        Name = name;
        Preis = preis;
    }

    public string Name { get; }
    public double Preis { get; }
}