using CryptoApp.Models.Enums;

namespace CryptoApp.Models.Models
{
    public class OperationModel
    {
        public long UserId { get; set; }
        
        public Operation Operation { get; set; }
        
        public Currency Currency { get; set; }
        
        public decimal Value { get; set; }
        
        public string Description { get; set; }
    }
}