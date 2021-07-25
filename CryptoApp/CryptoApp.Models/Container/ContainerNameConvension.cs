namespace CryptoApp.Models.Container
{
    public static class ContainerNameConvension
    {
        public static string GetCommandName(string commandName) => $"Command: {commandName}";
    }
}