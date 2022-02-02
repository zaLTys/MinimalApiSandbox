using MinimalApiSandbox.Model;

namespace MinimalApiSandbox.Repositories;

public interface IPersonsRepository
{
    Task<bool> CreateAsync(Person? person);
    Task<Person> UpdateAsync(Guid id, Person person);
    Task<List<Person>> GetAllAsync();
    Task<Person> GetByIdAsync(Guid id);
    Task<bool> Delete(Guid id);
}