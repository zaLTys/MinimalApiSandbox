using Microsoft.AspNetCore.Mvc;
using MinimalApiSandbox.Extensions;
using MinimalApiSandbox.Model;
using MinimalApiSandbox.Repositories;

namespace MinimalApiSandbox.EndpointDefinitions
{
    public class PersonsEndpointDefinition : IEndpointDefinition
    {
        public void DefineServices(IServiceCollection services)
        {
            services.AddSingleton<PersonsRepository>();
        }

        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet("/persons", async ([FromServices] PersonsRepository repo) => await repo.GetAllAsync());

            app.MapGet("/persons/{id}", async ([FromServices] PersonsRepository repo, Guid id) =>
            {
                var person = await repo.GetByIdAsync(id);
                return person is not null ? Results.Ok(person) : Results.NotFound();
            });

            app.MapPost("/persons", async ([FromServices] PersonsRepository repo, Person person) =>
            {
                await repo.CreateAsync(person);
                return Results.Created($"/persons/{person.Id}", person);
            });

            app.MapPut("/persons/{id}", async ([FromServices] PersonsRepository repo, Guid id, Person updatedPerson) =>
            {
                var person = await repo.GetByIdAsync(id);
                if (person is null)
                {
                    return Results.NotFound();
                }

                await repo.UpdateAsync(id, updatedPerson);
                return Results.Ok(updatedPerson);
            });

            app.MapDelete("/persons/{id}", async ([FromServices] PersonsRepository repo, Guid id) =>
            {
                await repo.Delete(id);
                return Results.Ok();
            });
        }
    }
}
