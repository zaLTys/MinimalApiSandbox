using Nethereum.HdWallet;

namespace MinimalApiSandbox.Services
{
    public interface INethereumService
    {
        Task<string> GetBalanceAsync(string address);
        Task<string> CreateWalletAsync(string password);
        Task<Wallet> LoadWalletFromFile(string nameOfWalletFile, string pass);
        Task<Dictionary<string, string>> LoadWordsFromFile(string nameOfWalletFile,string pass);
    }
}
