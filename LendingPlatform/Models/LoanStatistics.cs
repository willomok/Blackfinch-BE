namespace LendingPlatform.Models
{
    public class LoanStatistics
    {
        public int TotalApplications { get; set; }
        public int SuccessfulApplications { get; set; }
        public int UnsuccessfulApplications { get; set; }
        public decimal TotalLoanAmount { get; set; }
        public decimal AverageLTV { get; set; }
    }
}
