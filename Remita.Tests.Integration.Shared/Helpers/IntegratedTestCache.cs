using NormandyHostelManager.Tests.Integration.Shared.Helpers.Interfaces;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace Remita.Tests.Integration.Shared.Helpers;
public class IntegratedTestCache : IIntegratedTestCache
{
    private readonly ConcurrentDictionary<(Type, string?), List<object>> _inMemoryCache = new ConcurrentDictionary<(Type, string?), List<object>>();
    private readonly Random _random = new Random();


    public void Add<T>(T item, string? secondaryKey = null) where T : class
    {
        _inMemoryCache.GetOrAdd((typeof(T), secondaryKey), new List<object>()).Add(item);
    }

    public void AddRange<T>(List<T> list, string? secondaryKey = null) where T : class
    {
        _inMemoryCache.GetOrAdd((typeof(T), secondaryKey), new List<object>()).AddRange(list);
    }

    public ReadOnlyCollection<T> Get<T>(string? secondaryKey = null) where T : class
    {
        var list = _inMemoryCache.GetValueOrDefault((typeof(T), secondaryKey), null);

        if (list == null || list.Count == 0)
        {
            throw new InvalidOperationException($"List of type {typeof(T)} not initialized.");
        }
        return list.Cast<T>().ToList().AsReadOnly();
    }

    public T GetRandom<T>(string? secondaryKey = null) where T : class
    {
        var list = _inMemoryCache.GetValueOrDefault((typeof(T), secondaryKey), null);

        if (list == null || list.Count == 0)
        {
            throw new InvalidOperationException($"List with type {typeof(T)} not initialized.");
        }
        return list.Cast<T>().ToList()[_random.Next(list.Count)];
    }

    public ReadOnlyCollection<T> GetRandom<T>(int howMany, string? secondaryKey = null) where T : class
    {
        var list = _inMemoryCache.GetValueOrDefault((typeof(T), secondaryKey), null);

        if (list == null || howMany > list.Count)
        {
            throw new InvalidOperationException($"List with type {typeof(T)} not initialized.");
        }
        var distinctListPositions = new HashSet<int>();
        while (distinctListPositions.Count < howMany)
        {
            distinctListPositions.Add(_random.Next(list.Count));
        }
        var returnList = new List<object>();
        foreach (var index in distinctListPositions)
        {
            returnList.Add(list[index]);
        }
        return returnList.Cast<T>().ToList().AsReadOnly();
    }
}