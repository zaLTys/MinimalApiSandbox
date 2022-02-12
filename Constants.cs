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

    public static class PersonsEndpoints
    {
        public const string GetAllAsync = "/persons";
        public const string GetByIdAsync = "/persons/{id}";
        public const string CreateAsync = "/persons";
        public const string UpdateAsync = "/persons/{id}";
        public const string DeleteAsync = "/persons/{id}";
    }
}
