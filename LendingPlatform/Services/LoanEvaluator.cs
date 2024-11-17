using LendingPlatform.Models;

namespace LendingPlatform.Services
{
    public static class LoanEvaluator
    {
        public static bool Evaluate(LoanApplication loan)
        {
            if (loan.LoanAmount > 1500000 || loan.LoanAmount < 100000)
                return false;

            if (loan.LoanAmount >= 1000000)
                return loan.LTV <= 60 && loan.CreditScore >= 950;

            if (loan.LTV < 60)
                return loan.CreditScore >= 750;

            if (loan.LTV < 80)
                return loan.CreditScore >= 800;

            if (loan.LTV < 90)
                return loan.CreditScore >= 900;

            return false; // LTV 90% or more
        }
    }
}
