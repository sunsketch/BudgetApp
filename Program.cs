namespace Expense_tracking_app
{
    using System;
    using System.Collections.Generic;

    public enum ExpenseCategory
    {
        Housing,
        Transportation,
        Food,
        Utilities,
        Healthcare,
        Entertainment,
        Education,
        Savings,
        Debts,
        PersonalCare,
        ClothingAndAccessories,
        GiftsAndDonations,
        Travel,
        Taxes,
        Miscellaneous
    }
    public class Expense
    {
        public DateTime Date { get; set; }
        public ExpenseCategory Category { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
    class Program
    {
        static List<Expense> expenses = new List<Expense>();

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Expense Tracker Application:");
                Console.WriteLine("1. Add Expense");
                Console.WriteLine("2. View Expenses");
                Console.WriteLine("3. Edit Expense");
                Console.WriteLine("4. Delete Expense");
                Console.WriteLine("5. Filter Expenses by Category");
                Console.WriteLine("6. Save Expenses to File");
                Console.WriteLine("7. Load Expenses from File");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddExpense();
                        break;
                    case "2":
                        ViewExpenses();
                        break;
                    case "3":
                        EditExpense();
                        break;
                    case "4":
                        DeleteExpense();
                        break;
                    case "5":
                        Console.WriteLine("Select a category to filter expenses:");
                        foreach (ExpenseCategory expenseCategory in Enum.GetValues(typeof(ExpenseCategory)))
                        {
                            Console.WriteLine($"{(int)expenseCategory}. {expenseCategory}");
                        }
                        Console.Write("Category: ");
                        if (Enum.TryParse(Console.ReadLine(), out ExpenseCategory filterCategory))
                        {
                            FilterExpensesByCategory(filterCategory);
                        }
                        else
                        {
                            Console.WriteLine("Invalid category. Please select a valid category.");
                        }
                        break;
                    case "6":
                        SaveExpensesToFile("Expenses.txt");
                        break;
                    case "7":
                        LoadExpensesFromFile("Expenses.txt");
                        break;
                    case "8":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void AddExpense()
        {
            Console.Write("Enter the date (MM/dd/yyyy): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Select a category:");
                foreach (ExpenseCategory expenseCategory in Enum.GetValues(typeof(ExpenseCategory)))
                {
                    Console.WriteLine($"{(int)expenseCategory}. {expenseCategory}");
                }
                Console.Write("Category: ");
                if (Enum.TryParse(Console.ReadLine(), out ExpenseCategory category))
                {
                    Console.Write("Enter the description: ");
                    string description = Console.ReadLine();
                    Console.Write("Enter the amount: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    {
                        expenses.Add(new Expense
                        {
                            Date = date,
                            Category = category,
                            Description = description,
                            Amount = amount
                        });
                        Console.WriteLine("Expense added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount. Please enter a valid number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid category. Please select a valid category.");
                }
            }
            else
            {
                Console.WriteLine("Invalid date format. Please use MM/dd/yyyy.");
            }
        }
        static void ViewExpenses()
        {
            Console.WriteLine("Expenses:");
            foreach (var expense in expenses)
            {
                Console.WriteLine($"Date: {expense.Date.ToShortDateString()}");
                Console.WriteLine($"Category: {expense.Category}");
                Console.WriteLine($"Description: {expense.Description}");
                Console.WriteLine($"Amount: {expense.Amount:C}");
                Console.WriteLine();
            }
        }
        static void EditExpense()
        {
            Console.Write("Enter the index of the expense you want to edit: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < expenses.Count)
            {
                Expense expenseToEdit = expenses[index];
                Console.WriteLine("Editing expense:");
                Console.WriteLine($"Date: {expenseToEdit.Date.ToShortDateString()}");
                Console.WriteLine($"Category: {expenseToEdit.Category}");
                Console.WriteLine($"Description: {expenseToEdit.Description}");
                Console.WriteLine($"Amount: {expenseToEdit.Amount:C}");

                Console.Write("Enter the new date (MM/dd/yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime newDate))
                {
                    expenseToEdit.Date = newDate;
                }
                Console.Write("Enter the new description: ");
                expenseToEdit.Description = Console.ReadLine();
                Console.Write("Enter the new amount: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal newAmount))
                {
                    expenseToEdit.Amount = newAmount;
                }

                Console.WriteLine("Expense edited successfully!");
            }
            else
            {
                Console.WriteLine("Invalid index. Please enter a valid expense index.");
            }
        }
        static void DeleteExpense()
        {
            Console.Write("Enter the index of the expense you want to delete: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < expenses.Count)
            {
                Expense expenseToDelete = expenses[index];
                expenses.Remove(expenseToDelete);
                Console.WriteLine("Expense deleted successfully!");
            }
            else
            {
                Console.WriteLine("Invalid index. Please enter a valid expense index.");
            }
        }
        static void FilterExpensesByCategory(ExpenseCategory category)
        {
            Console.WriteLine($"Expenses in the {category} category:");
            foreach (var expense in expenses)
            {
                if (expense.Category == category)
                {
                    Console.WriteLine($"Date: {expense.Date.ToShortDateString()}");
                    Console.WriteLine($"Description: {expense.Description}");
                    Console.WriteLine($"Amount: {expense.Amount:C}");
                    Console.WriteLine();
                }
            }
        }
        static void SaveExpensesToFile(string fileName)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
            {
                foreach (var expense in expenses)
                {
                    file.WriteLine($"{expense.Date},{expense.Category},{expense.Description},{expense.Amount}");
                }
            }
            Console.WriteLine("Expenses saved to the file.");
        }
        static void LoadExpensesFromFile(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                string[] lines = System.IO.File.ReadAllLines(fileName);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        DateTime date;
                        if (DateTime.TryParse(parts[0], out date) &&
                            Enum.TryParse(parts[1], out ExpenseCategory category) &&
                            decimal.TryParse(parts[3], out decimal amount))
                        {
                            expenses.Add(new Expense
                            {
                                Date = date,
                                Category = category,
                                Description = parts[2],
                                Amount = amount
                            });
                        }
                    }
                }
                Console.WriteLine("Expenses loaded from the file.");
            }
            else
            {
                Console.WriteLine("The file does not exist. No expenses loaded.");
            }
        }
    }
}