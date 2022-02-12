namespace MinimalApiSandbox
{
    public static class AppConfiguration
    {
        public const string NetworkUrl = "NetworkUrl";
        public const string WorkingDirectory = "WorkingDirectory";
    }

    public static class NethereumEndpoints
    {
        public const string GetBalance = "/balance";
        public const string CreateWallet = "/createwallet";
        public const string LoadWallet = "/loadwalletfromfile";
        public const string LoadWords = "/loadwordsfromfile";
    } 
}
