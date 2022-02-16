using MinimalApiSandbox.Helpers;
using NBitcoin;
using Nethereum.HdWallet;
using Nethereum.Web3;

namespace MinimalApiSandbox.Services
{
    public class NethereumService : INethereumService
    {
        private readonly string _path;
        private readonly Web3 _web3;

        public NethereumService(IConfiguration config)
        {
            _path = config.GetSection(AppConfiguration.WorkingDirectory).Value;
            _web3 = new Web3(config.GetSection(AppConfiguration.NetworkUrl).Value);

        }

        public async Task<string> CreateWalletAsync(string password)
        {
            FileHelper.CheckDirectory(_path);
            Wallet wallet = new Wallet(Wordlist.English, WordCount.Twelve);
            string words = string.Join(" ", wallet.Words);
            string fileName = string.Empty;

            try
            {
                fileName = FileHelper.SaveWalletToJsonFile(wallet, password, _path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return await Task.FromResult($"New Wallet was created successfully! " +
                $"Write down the following mnemonic words and keep them in the save place." +
                $" { words} Now the Seed: {wallet.Seed}");
        }

        public async Task<Wallet> LoadWalletFromFile(string nameOfWalletFile, string pass)
        {
            FileHelper.CheckDirectory(_path);
            var wallet = FileHelper.LoadWalletFromJsonFile(nameOfWalletFile, _path, pass);
            return await Task.FromResult(wallet);
        }

        public async Task<Dictionary<string, string>> LoadWordsFromFile(string nameOfWalletFile, string pass)
        {
            FileHelper.CheckDirectory(_path);
            var wallet = FileHelper.LoadWalletFromJsonFile(nameOfWalletFile, _path, pass);
            var result = new Dictionary<string, string>();

            for (int i = 0; i < 20; i++)
            {
                result.Add(
                    wallet.GetAccount(i).Address,
                    wallet.GetAccount(i).PrivateKey);
            }
            return await Task.FromResult(result);
        }

        public async Task<string> GetBalanceAsync(string address = "0x948B1Ee102E69A3e7228cD2C7323FB6F87ad43B9") //test
        {
            try
            {
                var balance = await _web3.Eth.GetBalance.SendRequestAsync(address);
                Console.WriteLine($"Balance in Wei: {balance.Value}");

                var etherAmount = Web3.Convert.FromWei(balance.Value);
                return $"Balance in Ether: {etherAmount}";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
