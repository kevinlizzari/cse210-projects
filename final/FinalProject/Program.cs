using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class FinanceTracker
{
    private List<Transaction> transactions;

    public List<Transaction> Transactions { get { return transactions; } }

    public FinanceTracker()
    {
        transactions = new List<Transaction>();
    }

    public void AddTransaction(Transaction transaction)
    {
        transactions.Add(transaction);
    }

    //public void RemoveTransaction(Transaction transaction)
    //{
    //    transactions.Remove(transaction);
    //}

    public void DisplayTransactions()
    {
        Console.WriteLine("Transaction History:");
        foreach (var transaction in transactions)
        {
            Console.WriteLine(transaction);
        }
    }

    public decimal CalculateTotalIncome()
    {
        return transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
    }

    public decimal CalculateTotalExpenses()
    {
        return transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
    }

    public void SaveData(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var transaction in transactions)
            {
                writer.WriteLine($"{transaction.Amount},{transaction.Type},{transaction.Category},{transaction.Date}");
            }
        }

        Console.WriteLine("Data saved successfully.");
    }

    public void LoadData(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("No data file found. Starting with an empty finance tracker.");
            return;
        }

        transactions.Clear();

        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 4)
                {
                    decimal amount = decimal.Parse(parts[0]);
                    TransactionType type = (TransactionType)Enum.Parse(typeof(TransactionType), parts[1]);
                    string category = parts[2];
                    DateTime date = DateTime.Parse(parts[3]);

                    Transaction transaction = new Transaction(amount, type, category, date);
                    transactions.Add(transaction);
                }
            }
        }

        Console.WriteLine("Data loaded successfully.");
    }
}

class Transaction
{
    public decimal Amount { get; }
    public TransactionType Type { get; }
    public string Category { get; }
    public DateTime Date { get; }

    public Transaction(decimal amount, TransactionType type, string category, DateTime date)
    {
        Amount = amount;
        Type = type;
        Category = category;
        Date = date;
    }

    public override string ToString()
    {
        return $"Amount: {Amount:C}, Type: {Type}, Category: {Category}, Date: {Date.ToShortDateString()}";
    }
}

class IncomeTransaction : Transaction
{
    public string Source { get; }

    public IncomeTransaction(decimal amount, string category, DateTime date, string source)
        : base(amount, TransactionType.Income, category, date)
    {
        Source = source;
    }

    public override string ToString()
    {
        return $"Amount: {Amount:C}, Type: {Type}, Category: {Category}, Date: {Date.ToShortDateString()}, Source: {Source}";
    }
}

class ExpenseTransaction : Transaction
{
    public string Description { get; }

    public ExpenseTransaction(decimal amount, string category, DateTime date, string description)
        : base(amount, TransactionType.Expense, category, date)
    {
        Description = description;
    }

    public override string ToString()
    {
        return $"Amount: {Amount:C}, Type: {Type}, Category: {Category}, Date: {Date.ToShortDateString()}, Description: {Description}";
    }
}

enum TransactionType
{
    Income,
    Expense
}

class FinanceReport
{
    public FinanceTracker Tracker { get; }

    public FinanceReport(FinanceTracker tracker)
    {
        Tracker = tracker;
    }

    public void GenerateReport()
    {
        Console.WriteLine("Finance Report:");
        Console.WriteLine("----------------");

        decimal totalIncome = Tracker.CalculateTotalIncome();
        decimal totalExpenses = Tracker.CalculateTotalExpenses();
        decimal balance = totalIncome - totalExpenses;

        Console.WriteLine($"Total Income: ${totalIncome:F2}");
        Console.WriteLine($"Total Expenses: ${totalExpenses:F2}");
        Console.WriteLine($"Balance: ${balance:F2}");
    }
}

class TransactionManager
{
    private FinanceTracker Tracker { get; }

    public TransactionManager(FinanceTracker tracker)
    {
        Tracker = tracker;
    }

    public void AddIncomeTransaction(decimal amount, string category, DateTime date, string source)
    {
        IncomeTransaction transaction = new IncomeTransaction(amount, category, date, source);
        Tracker.AddTransaction(transaction);
        Console.WriteLine("Income transaction added successfully.");
    }

    public void AddExpenseTransaction(decimal amount, string category, DateTime date, string description)
    {
        ExpenseTransaction transaction = new ExpenseTransaction(amount, category, date, description);
        Tracker.AddTransaction(transaction);
        Console.WriteLine("Expense transaction added successfully.");
    }

    public void DisplayTransactions()
    {
        Console.WriteLine("Transaction History:");
        foreach (var transaction in Tracker.Transactions)
        {
            Console.WriteLine(transaction);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        FinanceTracker financeTracker = new FinanceTracker();
        TransactionManager transactionManager = new TransactionManager(financeTracker);
        FinanceReport financeReport = new FinanceReport(financeTracker);

        Console.WriteLine("Personal Finance Tracker");
        Console.WriteLine("------------------------");

        Console.WriteLine("1. Add Income Transaction");
        Console.WriteLine("2. Add Expense Transaction");
        Console.WriteLine("3. Display Transactions");
        Console.WriteLine("4. Calculate Total Income");
        Console.WriteLine("5. Calculate Total Expenses");
        Console.WriteLine("6. Generate Finance Report");
        Console.WriteLine("7. Save Data");
        Console.WriteLine("8. Load Data");
        Console.WriteLine("9. Exit");

        while (true)
        {
            Console.WriteLine();
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter transaction amount: ");
                    decimal incomeAmount = decimal.Parse(Console.ReadLine());

                    Console.Write("Enter transaction category: ");
                    string incomeCategory = Console.ReadLine();

                    Console.Write("Enter transaction date (yyyy-mm-dd): ");
                    DateTime incomeDate = DateTime.Parse(Console.ReadLine());

                    Console.Write("Enter transaction source: ");
                    string source = Console.ReadLine();

                    transactionManager.AddIncomeTransaction(incomeAmount, incomeCategory, incomeDate, source);
                    break;

                case "2":
                    Console.Write("Enter transaction amount: ");
                    decimal expenseAmount = decimal.Parse(Console.ReadLine());

                    Console.Write("Enter transaction category: ");
                    string expenseCategory = Console.ReadLine();

                    Console.Write("Enter transaction date (yyyy-mm-dd): ");
                    DateTime expenseDate = DateTime.Parse(Console.ReadLine());

                    Console.Write("Enter transaction description: ");
                    string description = Console.ReadLine();

                    transactionManager.AddExpenseTransaction(expenseAmount, expenseCategory, expenseDate, description);
                    break;

                case "3":
                    transactionManager.DisplayTransactions();
                    break;

                case "4":
                    decimal totalIncome = financeTracker.CalculateTotalIncome();
                    Console.WriteLine($"Total Income: {totalIncome:C}");
                    break;

                case "5":
                    decimal totalExpenses = financeTracker.CalculateTotalExpenses();
                    Console.WriteLine($"Total Expenses: {totalExpenses:C}");
                    break;

                case "6":
                    financeReport.GenerateReport();
                    break;

                case "7":
                    Console.Write("Enter the filename to save the data: ");
                    string saveFilename = Console.ReadLine();
                    financeTracker.SaveData(saveFilename);
                    break;

                case "8":
                    Console.Write("Enter the filename to load the data: ");
                    string loadFilename = Console.ReadLine();
                    financeTracker.LoadData(loadFilename);
                    break;

                case "9":
                    Console.WriteLine("Exiting program. Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}