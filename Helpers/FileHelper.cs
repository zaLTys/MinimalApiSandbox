using Nethereum.HdWallet;
using Newtonsoft.Json;
using Rijndael256;

namespace MinimalApiSandbox.Helpers
{
    public static class FileHelper
    {
        public static string SaveWalletToJsonFile(Wallet wallet, string password, string pathfile)
        {
            string words = string.Join(" ", wallet.Words);
            var encryptedWords = Rijndael.Encrypt(words, password, KeySize.Aes256);
            string date = DateTime.Now.ToString();
            var walletJsonData = new { encryptedWords, date };
            string json = JsonConvert.SerializeObject(walletJsonData);
            Random random = new Random();
            var fileName =
                $"EthereumWallet_{ DateTime.Now.Year}-{ DateTime.Now.Month}-{ DateTime.Now.Day}" +
                $"-{ DateTime.Now.Hour}-{ DateTime.Now.Minute}-{ DateTime.Now.Second}" +
                $"-{ random.Next(0, 1000)}.json";
            File.WriteAllText(Path.Combine(pathfile, fileName), json);
            return $"Wallet saved in file: {fileName}";
        }

        public static Wallet LoadWalletFromJsonFile(string nameOfWalletFile, string path, string pass)
        {
            string pathToFile = Path.Combine(path, nameOfWalletFile);
            string words = string.Empty;
            Console.WriteLine($"Read from {pathToFile}");
            try
            {
                string line = File.ReadAllText(pathToFile);
                dynamic results = JsonConvert.DeserializeObject<dynamic>(line);
                string encryptedWords = results.encryptedWords;
                words = Rijndael.Decrypt(encryptedWords, pass, KeySize.Aes256);
                string dataAndTime = results.date;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR!" + e);
            }

            return Recover(words);
        }
        private static Wallet Recover(string words)
        {
            Wallet wallet = new Wallet(words, null);
            Console.WriteLine("Wallet was successfully recovered.");
            Console.WriteLine("Words: " + string.Join(" ", wallet.Words));
            Console.WriteLine("Seed: " + string.Join(" ", wallet.Seed));
            Console.WriteLine();
            return wallet;
        }

        public static void CheckDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
