// Name: Kaleb
// Collect expenses and display their total and largest value.

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class ai_ExpenseTracker {
    private static class Expense {
        private final String description;
        private final BigDecimal amount;

        private Expense(String description, BigDecimal amount) {
            this.description = description;
            this.amount = amount;
        }
    }

    private static List<Expense> collectExpenses(Scanner scanner) {
        List<Expense> expenses = new ArrayList<>();

        while (true) {
            System.out.print("Enter an expense description (or 'done' to finish): ");
            String description = scanner.nextLine().trim();

            if (description.equalsIgnoreCase("done")) {
                return expenses;
            }

            if (description.isEmpty()) {
                System.out.println("Description cannot be empty.");
                continue;
            }

            while (true) {
                System.out.print("Enter the amount for " + description + ": ");
                String amountText = scanner.nextLine().trim();

                try {
                    BigDecimal amount = new BigDecimal(amountText);
                    if (amount.signum() <= 0) {
                        System.out.println("Amount must be a positive number.");
                        continue;
                    }

                    expenses.add(new Expense(description, amount));
                    break;
                } catch (NumberFormatException exception) {
                    System.out.println("Please enter a valid number.");
                }
            }
        }
    }

    private static void summaryReport(List<Expense> expenses) {
        if (expenses.isEmpty()) {
            System.out.println("No expenses were entered.");
            return;
        }

        System.out.println("\nExpense summary:");
        BigDecimal total = BigDecimal.ZERO;
        Expense largestExpense = expenses.get(0);

        for (Expense expense : expenses) {
            System.out.printf("%s: $%.2f%n", expense.description, expense.amount);
            total = total.add(expense.amount);

            if (expense.amount.compareTo(largestExpense.amount) > 0) {
                largestExpense = expense;
            }
        }

        System.out.printf("Total spent: $%.2f%n", total);
        System.out.printf(
                "Largest expense: %s: $%.2f%n",
                largestExpense.description,
                largestExpense.amount);
    }

    public static void main(String[] args) {
        try (Scanner scanner = new Scanner(System.in)) {
            List<Expense> expenses = collectExpenses(scanner);
            summaryReport(expenses);
        }
    }
}
