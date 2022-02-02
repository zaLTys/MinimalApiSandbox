using MinimalApiSandbox.Model;

namespace MinimalApiSandbox.Repositories;

class PersonsRepository : IPersonsRepository
{
    private readonly Dictionary<Guid, Person> _persons = new();
    public Task<bool> CreateAsync(Person? person)
    {
        if (person == null)
        {
            return Task.FromResult(false);
        }
        _persons[person.Id] = person;
        return Task.FromResult(true);
    }

    public async Task<Person> UpdateAsync(Guid id, Person person)
    {
        var existingPerson = await GetByIdAsync(id);
        _persons[existingPerson.Id] = person;
        return await Task.FromResult(person);
    }

    public async Task<List<Person>> GetAllAsync()
    {
        var persons = _persons.Select(x => x.Value).ToList();
        return await Task.FromResult(persons);
    }

    public async Task<Person> GetByIdAsync(Guid id)
    {
        var person = _persons.FirstOrDefault(x => x.Key == id).Value;
        return await Task.FromResult(person);
    }

    public async Task<bool> Delete(Guid id)
    {
        await Task.FromResult(_persons.Remove(id));
        return true;
    }
}