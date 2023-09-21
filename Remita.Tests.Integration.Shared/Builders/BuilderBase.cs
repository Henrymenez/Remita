using Bogus;

namespace Remita.Tests.Integration.Shared.Builders;
public abstract class BuilderBase<T> where T : class
{
    protected Faker<T> _faker;
    protected List<T> _instances;
    protected Random _random;

    public BuilderBase()
    {
        _random = new Random();
        _faker = new Faker<T>();
        _instances = new List<T>();
        ConfigureRule(_faker);
    }

    abstract public void ConfigureRule(Faker<T> faker);

    public T SingleRandom(IList<T> list)
    {

        if (list.Count == 0)
        {
            throw new ArgumentNullException("List is empty");
        }
        if (list.Count == 1)
        {
            return list[0];
        }
        return list[_random.Next(0, list.Count)];
    }

    public T? Single()
    {
        if (!_instances.Any()) throw new ArgumentNullException("No instances generated");
        return _instances.FirstOrDefault();
    }

    public List<T> GetAll()
    {
        if (!_instances.Any()) throw new ArgumentNullException("No instances generated");
        return _instances;
    }

    public BuilderBase<T> Generate()
    {
        var instance = _faker.Generate();
        _instances.Add(instance);
        return this;
    }

    public BuilderBase<T> Generate(int numberInstancesToGenerate)
    {
        var instances = _faker.Generate(numberInstancesToGenerate);
        _instances.AddRange(instances);
        return this;
    }

    public BuilderBase<T> Generate(Action<T> modify)
    {
        var instance = _faker.Generate();
        modify(instance);
        _instances.Add(instance);
        return this;
    }
    public BuilderBase<T> Generate(int numberInstancesToGenerate, Action<List<T>> modify)
    {
        var instances = _faker.Generate(numberInstancesToGenerate);
        modify(instances);
        _instances.AddRange(instances);
        return this;
    }

}
