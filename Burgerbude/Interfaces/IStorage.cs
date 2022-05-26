using System.ComponentModel;
using Burgerbude.Models;

namespace Burgerbude.Interfaces;

public interface IStorage<T> where T : class, IStorageItem 
{
    T Get(Guid id);
    IEnumerable<T> GetAll();
    Guid Add(T item);
    void Remove(Guid id);
}