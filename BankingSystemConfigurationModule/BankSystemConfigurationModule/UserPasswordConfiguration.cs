namespace BankSystemConfigurationModule
{
    public class UserPasswordConfiguration : BaseConfiguration, IUserPasswordConfiguration
    {
        public UserPasswordConfiguration(int id) : base(id)
        {

        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public override string GetConfigurationInfo()
        {
            return string.IsNullOrEmpty(LastLog) || string.IsNullOrWhiteSpace(LastLog)
                ? $"Configuration id: {Id}; Log: no log"
                : $"Configuration id: {Id}; Log: {LastLog}";
        }
    }
}