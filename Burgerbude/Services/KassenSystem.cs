using Burgerbude.Interfaces;
using Burgerbude.Models;

namespace Burgerbude.Services;

public class KassenSystem : IKassenSchnittstelle
{
    private readonly IStorage<Bestellung> _storage;

    public KassenSystem(IStorage<Bestellung> storage)
    {
        _storage = storage;
    }

    public Guid SelfserviceBestellungAufgeben(IEnumerable<IZutat> zutaten)
    {
        var bestellung = BestellungErstellen(zutaten);
        return _storage.Add(bestellung);
    }

    private Bestellung BestellungErstellen(IEnumerable<IZutat> zutaten)
    {
        var zutatenArray = zutaten.ToArray();

        var gesamtPreis = zutatenArray.Sum(x => x.Preis);

        return new Bestellung
        {
            GesamtPreis = gesamtPreis,
            Zutaten = zutatenArray
        };
    }
}