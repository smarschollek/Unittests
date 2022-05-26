using Burgerbude.Interfaces;

namespace Burgerbude.Models;

public class Bestellung : IStorageItem
{
    public Guid Id { get; set; }
    public IEnumerable<IZutat> Zutaten { get; set; }
    public double GesamtPreis { get; set; }
}