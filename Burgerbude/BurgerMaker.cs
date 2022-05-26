using Burgerbude.Interfaces;

namespace Burgerbude;

public class BurgerMaker
{
    private readonly IKüchenSchnittstelle _küchenSchnittstelle;
    private readonly IKassenSchnittstelle _kassenSchnittstelle;
    private readonly List<IZutat> _zutaten;

    public BurgerMaker(IZutat buns, IKüchenSchnittstelle küchenSchnittstelle, IKassenSchnittstelle kassenSchnittstelle)
    {
        _küchenSchnittstelle = küchenSchnittstelle;
        _kassenSchnittstelle = kassenSchnittstelle;
        _zutaten = new List<IZutat>();
        _zutaten.Add(buns);
    }

    public IReadOnlyList<IZutat> Zutaten => _zutaten;

    public void ZutatHinzufügen(IZutat zutat)
    {
        _zutaten.Add(zutat);
    }

    public void ZutatEntfernen(int index)
    {
        if (index == 0) throw new Exception("Buns können nicht entfernt werden.");

        _zutaten.RemoveAt(index);
    }

    public Guid Bestellen()
    {
        _küchenSchnittstelle.BestellungAufgeben(InKüchenSpracheKonvertieren(_zutaten));
        return _kassenSchnittstelle.SelfserviceBestellungAufgeben(_zutaten);
    }

    private IEnumerable<string> InKüchenSpracheKonvertieren(List<IZutat> zutaten)
    {
        var result = new List<string>();
        var buns = zutaten[0];
        result.Add($"Einmal {buns.Name} mit:");

        var grouped = zutaten
            .GetRange(1, zutaten.Count-1)
            .GroupBy(x => x.Name);

        foreach (var item in grouped)
        {
            var menge = item.Count();
            switch (menge)
            {
                case 1:
                    result.Add($"{item.Key}");
                    break;
                case 2:
                    result.Add($"Doppelt {item.Key}");
                    break;
                case 3:
                    result.Add($"Tripple {item.Key}");
                    break;
                default:
                    result.Add($"{menge}x {item.Key}");
                    break;
            }
        }

        result[^1] = $"und {result.Last()}";

        return result;
    }
}