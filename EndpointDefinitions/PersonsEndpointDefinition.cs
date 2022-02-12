using MinimalApiSandbox.Extensions;
using MinimalApiSandbox.Models;
using MinimalApiSandbox.Repositories;

namespace MinimalApiSandbox.EndpointDefinitions
{
    public class PersonsEndpointDefinition : IEndpointDefinition
    {
        public void DefineServices(IServiceCollection services)
        {
            services.AddSingleton<IPersonsRepository, PersonsRepository>();
        }

        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet("/persons", GetAllAsync);
            app.MapGet("/persons/{id}", GetByIdAsync);
            app.MapPost("/persons", CreateAsync);
            app.MapPut("/persons/{id}", UpdateAsync);
            app.MapDelete("/persons/{id}", DeleteAsync);
        }

        internal async Task<IResult> GetAllAsync(IPersonsRepository repo)
        {
            var result = await repo.GetAllAsync();
            return Results.Ok(result);
        }

        internal async Task<IResult> GetByIdAsync(IPersonsRepository repo, Guid id)
        {
            var person = await repo.GetByIdAsync(id);
            return person is not null ? Results.Ok(person) : Results.NotFound();
        }

        internal async Task<IResult> CreateAsync(IPersonsRepository repo, Person person)
        {
            await repo.CreateAsync(person);
            return Results.Created($"/persons/{person.Id}", person);
        }

        internal async Task<IResult> UpdateAsync(IPersonsRepository repo, Guid id, Person updatedPerson)
        {
            var person = await repo.GetByIdAsync(id);
            if (person is null)
            {
                return Results.NotFound();
            }

            await repo.UpdateAsync(id, updatedPerson);
            return Results.Ok(updatedPerson);
        }

        internal async Task<IResult> DeleteAsync(IPersonsRepository repo, Guid id)
        {
            await repo.Delete(id);
            return Results.Ok();
        }
    }
}
