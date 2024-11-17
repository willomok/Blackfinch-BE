namespace LendingPlatform.Models
{
    public class LoanApplication
    {
        public int Id { get; set; } // Primary Key
        public decimal LoanAmount { get; set; }
        public decimal AssetValue { get; set; }
        public int CreditScore { get; set; }
        public decimal LTV { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
