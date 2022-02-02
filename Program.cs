using MinimalApiSandbox.Extensions;
using MinimalApiSandbox.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointDefinitions(typeof(Person)); //AssemblyScan

var app = builder.Build();
app.UseEndpointDefinitions();

app.UseHttpsRedirection();
app.Run();