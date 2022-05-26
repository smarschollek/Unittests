namespace Burgerbude.Interfaces;

public interface IKüchenSchnittstelle
{
    void BestellungAufgeben(IEnumerable<string> zutaten);
}