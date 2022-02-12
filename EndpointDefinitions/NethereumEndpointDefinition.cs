using MinimalApiSandbox.Extensions;
using MinimalApiSandbox.Repositories;
using MinimalApiSandbox.Services;

namespace MinimalApiSandbox.EndpointDefinitions
{
    public class NethereumEndpointDefinition : IEndpointDefinition
    {
        public void DefineServices(IServiceCollection services)
        {
            services.AddScoped<INethereumService, NethereumService>();
        }

        public void DefineEndpoints(WebApplication app)
        {
            app.MapGet("/balance", GetBalanceAsync);
            app.MapGet("/createwallet", CreateWalletAsync);
            app.MapGet("/loadwalletfromfile", LoadWalletFromFile);
            app.MapGet("/loadwordsfromfile", LoadWordsFromFile);
        }

        internal async Task<IResult> GetBalanceAsync(INethereumService _nethereumService)
        {
            var result = await _nethereumService.GetBalanceAsync();
            return Results.Ok(result);
        }

        internal async Task<IResult> CreateWalletAsync(INethereumService _nethereumService, string password)
        {
            var result = await _nethereumService.CreateWalletAsync(password);
            return Results.Ok(result);
        }

        internal async Task<IResult> LoadWalletFromFile(INethereumService _nethereumService, string nameOfWalletFile, string path, string password)
        {
            var result = await _nethereumService.LoadWalletFromFile(nameOfWalletFile, password);
            return Results.Ok(result);
        }
        internal async Task<IResult> LoadWordsFromFile(INethereumService _nethereumService, string nameOfWalletFile, string path, string password)
        {
            var result = await _nethereumService.LoadWordsFromFile(nameOfWalletFile, password);
            return Results.Ok(result);
        }

    }
}
