using System;
using LendingPlatform.Models;
using LendingPlatform.Services;
using LendingPlatform.Data;
using Microsoft.EntityFrameworkCore;

namespace LendingPlatform
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new LoanDbContext())
            {
                context.Database.EnsureCreated();

                Console.WriteLine("Welcome to the Lending Platform!");

                while (true)
                {
                    Console.WriteLine("\nEnter Loan Application Details:");

                    decimal loanAmount = GetDecimalInput("Loan Amount (in GBP): ");
                    decimal assetValue = GetDecimalInput("Asset Value (in GBP): ");
                    int creditScore = GetIntegerInput("Credit Score (1-999): ");

                    var loan = new LoanApplication
                    {
                        LoanAmount = loanAmount,
                        AssetValue = assetValue,
                        CreditScore = creditScore,
                        LTV = (loanAmount / assetValue) * 100,
                    };

                    loan.IsSuccessful = LoanEvaluator.Evaluate(loan);
                    context.LoanApplications.Add(loan);
                    context.SaveChanges();

                    Console.WriteLine($"\nApplication Result: {(loan.IsSuccessful ? "Approved" : "Declined")}");

                    DisplayStatistics(context);

                    Console.WriteLine("\nDo you want to enter another application? (y/n)");
                    string continueInput = Console.ReadLine() ?? string.Empty;

                        if (continueInput.Trim().ToLower() != "y")
                        {
                            Console.WriteLine("Exiting. Goodbye!");
                            break;
                        }
                }
            }
        }

        private static void DisplayStatistics(LoanDbContext context)
        {
            var stats = StatisticsService.CalculateStatistics(context.LoanApplications);

            Console.WriteLine("\n--- Statistics ---");
            Console.WriteLine($"Total Applications: {stats.TotalApplications}");
            Console.WriteLine($"Successful Applications: {stats.SuccessfulApplications}");
            Console.WriteLine($"Unsuccessful Applications: {stats.UnsuccessfulApplications}");
            Console.WriteLine($"Total Loan Amount: £{stats.TotalLoanAmount:N2}");
            Console.WriteLine($"Average LTV: {stats.AverageLTV:N2}%");
        }

        private static decimal GetDecimalInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal result))
                    return result;

                Console.WriteLine("Invalid input. Please enter a valid decimal number.");
            }
        }

        private static int GetIntegerInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result) && result >= 1 && result <= 999)
                    return result;

                Console.WriteLine("Invalid input. Please enter a number between 1 and 999.");
            }
        }
    }
}
