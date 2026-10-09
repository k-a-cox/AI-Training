# Name: Kaleb
# Collect expenses and display their total and largest value.

import math


def collect_expenses() -> list[tuple[str, float]]:
    """Collect expense descriptions and positive amounts from the user."""
    expenses = []

    while True:
        description = input("Enter an expense description (or 'done' to finish): ")
        if description.strip().lower() == "done":
            return expenses

        while True:
            amount_text = input(f"Enter the amount for {description}: ")
            try:
                amount = float(amount_text)
            except ValueError:
                print("Please enter a valid number.")
                continue

            if not math.isfinite(amount) or amount <= 0:
                print("Amount must be a positive number.")
                continue

            expenses.append((description, amount))
            break


def summary_report(expenses: list[tuple[str, float]]) -> None:
    """Display each expense, their total, and the largest expense."""
    if not expenses:
        print("No expenses were entered.")
        return

    print("\nExpense summary:")
    for description, amount in expenses:
        print(f"{description}: ${amount:.2f}")

    total = sum(amount for _, amount in expenses)
    print(f"Total spent: ${total:.2f}")

    largest_expense = max(expenses, key=lambda expense: expense[1])
    print(
        f"Largest expense: {largest_expense[0]}: "
        f"${largest_expense[1]:.2f}"
    )


def main() -> None:
    """Run the expense tracker."""
    expenses = collect_expenses()
    summary_report(expenses)


if __name__ == "__main__":
    main()
