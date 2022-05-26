using System.Text.Json;
using Burgerbude.Interfaces;
using Burgerbude.Models;

namespace Burgerbude.Services;

public class FileStorage<T> : IStorage<T> where T : class, IStorageItem
{
    private readonly string _storageFile;
    private readonly List<T> _items;

    public FileStorage(string storageFile)
    {
        _storageFile = storageFile;
        var fileDoesNotExist = !File.Exists(storageFile);
        if (fileDoesNotExist)
        {
            File.Create(storageFile).Dispose();
            _items = new List<T>();
        }
        else
        {
            var json = File.ReadAllText(storageFile);
            if (string.IsNullOrEmpty(json))
            {
                _items = new List<T>();
            }
            else
            {
                _items = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            
        }
    }

    public T Get(Guid id)
    {
        var item = _items.Find(x => x.Id.Equals(id));
        if (item == null)
        {
            throw new Exception($"item with id {id} not found - check your storage");
        }

        return item;
    }

    public IEnumerable<T> GetAll()
    {
        return _items;
    }

    public Guid Add(T item)
    {
        item.Id = Guid.NewGuid();
        _items.Add(item);
        SaveChanges();
        return item.Id;
    }

    public void Remove(Guid id)
    {
        var index = _items.FindIndex(x => x.Id.Equals(id));
        if (index == -1)
        {
            throw new Exception($"item with id {id} not found - check your storage");
        }

        _items.RemoveAt(index);
        SaveChanges();
    }

    private void SaveChanges()
    {
        var json = JsonSerializer.Serialize(_items);
        File.WriteAllText(_storageFile, json);
    }
}