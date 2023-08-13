using System.Collections.ObjectModel;

namespace NormandyHostelManager.Tests.Integration.Shared.Helpers.Interfaces;
internal interface IIntegratedTestCache
{
    void Add<T>(T item, string? secondaryKey = null) where T : class;
    void AddRange<T>(List<T> list, string? secondaryKey = null) where T : class;
    ReadOnlyCollection<T> Get<T>(string? secondaryKey = null) where T : class;
    T GetRandom<T>(string? secondaryKey = null) where T : class;
    ReadOnlyCollection<T> GetRandom<T>(int howMany, string? secondaryKey = null) where T : class;
}
