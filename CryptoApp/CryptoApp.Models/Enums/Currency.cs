using System.ComponentModel;

namespace CryptoApp.Models.Enums
{
    public enum Currency
    {
        [Description("BTC")] BTC = 2,
        
        [Description("DOGE")] DOGE = 3,
        
        [Description("ADA")] ADA = 4,
        
        [Description("LINK")] LINK = 5
    }
}