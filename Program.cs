using MinimalApiSandbox.Extensions;
using MinimalApiSandbox.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointDefinitions(typeof(Person)); //AssemblyScan


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseEndpointDefinitions();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();



app.Run();