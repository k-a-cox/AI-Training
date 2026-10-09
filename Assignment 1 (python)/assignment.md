# Python Assignment 1: Personal Expense Tracker

## Goal

Write a Python program that helps a user record expenses and see how much they have spent. This assignment practices variables, input, conditionals, loops, lists, and functions.

## Problem

Create a command-line program that lets the user enter expenses one at a time. Each expense has a description and a positive amount. When the user is done, display every expense and the total amount spent.

## Requirements

1. Ask the user to enter an expense description and amount.
2. Keep asking for expenses until the user types `done` as the description.
3. Store each expense so it can be displayed later.
4. Reject amounts that are not valid positive numbers. Show a helpful message and ask for that expense again.
5. After the user is done, print each expense and the total, formatted as currency with two decimal places.
6. If the user does not enter any expenses, display a message explaining that there are no expenses to summarize.
7. Organize your program with at least two functions:
   - One function to collect expenses.
   - One function to display the summary and calculate or show the total.

## Example session

```text
Enter an expense description (or 'done' to finish): Lunch
Enter the amount: 12.50
Enter an expense description (or 'done' to finish): Bus fare
Enter the amount: 2.75
Enter an expense description (or 'done' to finish): done

Expense summary:
Lunch: $12.50
Bus fare: $2.75
Total spent: $15.25
```

Your prompts and formatting do not need to match the example exactly, as long as the program meets the requirements.

## What to submit

Submit your Python source file named `expense_tracker.py`. Include a short comment at the top with your name and a one-sentence description of the program.

## Suggested testing

- Enter several expenses, including amounts with decimal places.
- Try entering a word instead of an amount.
- Try entering zero or a negative amount.
- Type `done` before entering any expenses.

## Optional challenge

Show the largest expense in the summary. If there are no expenses, do not show a largest expense.

## Grading rubric

- Correctly collects and stores expense descriptions and amounts: 30%
- Validates input and handles invalid or non-positive amounts: 25%
- Displays the expense list and correctly formatted total: 25%
- Uses at least two functions and is readable: 20%
