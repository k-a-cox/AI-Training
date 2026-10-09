"""Name: Kaleb

Collect expense descriptions and amounts, then display the expenses and total.
"""

import argparse
from unittest.mock import patch


def collectExpenses() -> list[tuple[str, float]]:
    """Collect expense descriptions and amounts until the user is done."""
    expenses = []

    while True:
        description = input("Enter an expense description (or 'done' to finish): ")
        if description.lower() == "done" or description == "":
            break
        # try:
        amount = input("Enter the amount: ")
        while True:
            try:
                if float(amount) <= 0:
                    print("Amount must be positive. Please enter a positive number.")
                    amount = input("Enter the amount: ")
                else:
                    amount = float(amount)
                    break
            except ValueError:
                print("Invalid amount. Please enter a number.")
                amount = input("Enter the amount: ")

        expenses.append((description, amount))

    return expenses


def summaryReport(expenses: list[tuple[str, float]]) -> None:
    """Print the collected expenses."""
    if not expenses:
        print("No expenses were entered.")
        return
    print("You entered the following expenses:")
    for description, amount in expenses:
        print(description,f": ${amount:.2f}")
    total = sum(amount for _, amount in expenses)
    print(f"Total expenses: ${total:.2f}")

    largest_expense = max(expenses, key=lambda expense: expense[1])
    print(f"Largest expense: {largest_expense[0]}: ${largest_expense[1]:.2f}")


def main() -> None:
    """Run the program."""
    expenses = collectExpenses()
    summaryReport(expenses)


def testRoute() -> None:
    """Run the expense flow with sample inputs instead of prompting manually."""
    sample_inputs = ["Lunch", "12.50", "Bus fare", "2.75", "done"]
    with patch("builtins.input", side_effect=sample_inputs):
        expenses = collectExpenses()
    summaryReport(expenses)


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument(
        "--test",
        action="store_true",
        help="run the expense flow with sample inputs",
    )
    arguments = parser.parse_args()

    if arguments.test:
        testRoute()
    else:
        main()