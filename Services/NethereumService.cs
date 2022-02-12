using MinimalApiSandbox.Helpers;
using NBitcoin;
using Nethereum.HdWallet;
using Nethereum.Web3;
using Newtonsoft.Json;
using Rijndael256;

namespace MinimalApiSandbox.Services
{
    public class NethereumService : INethereumService
    {
        private readonly string _path;

        public NethereumService(IConfiguration config)
        {
            _path = config.GetSection("WorkingDirectory").Value;
        }

        public async Task<string> CreateWalletAsync(string password)
        {
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
            try
            {
                var wallet = FileHelper.LoadWalletFromJsonFile(nameOfWalletFile, _path, pass);
                return await Task.FromResult(wallet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Dictionary<string,string>> LoadWordsFromFile(string nameOfWalletFile, string pass)
        {
            try
            {
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
            catch (Exception)
            {
                throw;
            }

        }    

        public async Task<string> GetBalanceAsync() //test
        {
            var web3 = new Web3("https://mainnet.infura.io/v3/7238211010344719ad14a89db874158c");
            var balance = await web3.Eth.GetBalance.SendRequestAsync("0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae");
            Console.WriteLine($"Balance in Wei: {balance.Value}");

            var etherAmount = Web3.Convert.FromWei(balance.Value);
            return $"Balance in Ether: {etherAmount}";
        }
    }
}
