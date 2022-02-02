using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<PersonsRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

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

app.Run();