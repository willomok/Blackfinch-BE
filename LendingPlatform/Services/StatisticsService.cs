using System.Collections.Generic;
using System.Linq;
using LendingPlatform.Models;

namespace LendingPlatform.Services
{
    public static class StatisticsService
    {
        public static LoanStatistics CalculateStatistics(IQueryable<LoanApplication> applications)
        {
            // Load applications into memory for calculation
            var loanList = applications.AsEnumerable();

            var stats = new LoanStatistics
            {
                TotalApplications = loanList.Count(),
                SuccessfulApplications = loanList.Count(a => a.IsSuccessful),
                UnsuccessfulApplications = loanList.Count(a => !a.IsSuccessful),
                TotalLoanAmount = loanList.Sum(a => a.LoanAmount),
                AverageLTV = loanList.Any() 
                    ? loanList.Average(a => (a.LoanAmount / a.AssetValue) * 100) 
                    : 0
            };

            return stats;
        }
    }
}
