using System.ComponentModel;

namespace CryptoApp.Models.Enums
{
    public enum Operation
    {
        [Description("Добавить")] ADD = 1,
        [Description("Отнять")] TAKEAWAY = 2
    }
}