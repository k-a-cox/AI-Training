// Name: Kaleb
// Collect expenses and display their total and largest value.

using System;
using System.Collections.Generic;
using System.Globalization;

internal static class ExpenseTracker
{
    private sealed class Expense
    {
        public string Description { get; }
        public decimal Amount { get; }

        public Expense(string description, decimal amount)
        {
            Description = description;
            Amount = amount;
        }
    }

    private static List<Expense> CollectExpenses()
    {
        var expenses = new List<Expense>();

        while (true)
        {
            Console.Write("Enter an expense description (or 'done' to finish): ");
            string description = Console.ReadLine() ?? "";

            if (description.Trim().Equals("done", StringComparison.OrdinalIgnoreCase))
            {
                return expenses;
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Description cannot be empty.");
                continue;
            }

            while (true)
            {
                Console.Write($"Enter the amount for {description}: ");
                string amountText = Console.ReadLine() ?? "";

                if (!decimal.TryParse(
                        amountText,
                        NumberStyles.Number,
                        CultureInfo.InvariantCulture,
                        out decimal amount))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                if (amount <= 0)
                {
                    Console.WriteLine("Amount must be a positive number.");
                    continue;
                }

                expenses.Add(new Expense(description, amount));
                break;
            }
        }
    }

    private static void SummaryReport(List<Expense> expenses)
    {
        if (expenses.Count == 0)
        {
            Console.WriteLine("No expenses were entered.");
            return;
        }

        Console.WriteLine("\nExpense summary:");
        decimal total = 0;
        Expense largestExpense = expenses[0];

        foreach (Expense expense in expenses)
        {
            Console.WriteLine($"{expense.Description}: ${expense.Amount:F2}");
            total += expense.Amount;

            if (expense.Amount > largestExpense.Amount)
            {
                largestExpense = expense;
            }
        }

        Console.WriteLine($"Total spent: ${total:F2}");
        Console.WriteLine(
            $"Largest expense: {largestExpense.Description}: ${largestExpense.Amount:F2}");
    }

    private static void Main()
    {
        List<Expense> expenses = CollectExpenses();
        SummaryReport(expenses);
    }
}
