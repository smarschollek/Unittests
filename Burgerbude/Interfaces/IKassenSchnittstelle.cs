namespace Burgerbude.Interfaces;

public interface IKassenSchnittstelle
{
    Guid SelfserviceBestellungAufgeben(IEnumerable<IZutat> zutaten);
}