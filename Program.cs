using Spectre.Console;

class Program
{
    static List<Expense> expenses = StorageService.LoadExpenses();

    static void Main()
    {
        List<string> choice =
        [
            "Add Transaction",
            "View Transactions",
            "Delete Transaction",
            "View Log",
            "Exit",
        ];
        AnsiConsole.Write(new FigletText("Budget").Centered().Color(Color.Gold1));
        AnsiConsole.Write(new FigletText("Tracker").Centered().Color(Color.Green1));
        while (true)
        {
            string menuChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>().Title("\nWhat do you want to do?").AddChoices(choice)
            );

            switch (menuChoice)
            {
                case "Add Transaction":
                    AddTransactionMenu();
                    break;
                case "View Transactions":
                    ViewTransactions();
                    break;
                case "Delete Transaction":
                    DeleteTransaction();
                    break;
                case "View Log":
                    ViewLog();
                    break;
                case "Exit":
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }

            static void AddTransaction(TransactionType type)
            {
                string desc = AnsiConsole.Ask<string>("Description: ");

                decimal amt = AnsiConsole.Ask<decimal>("Amount: ");

                var transaction = new Expense
                {
                    Id = Guid.NewGuid(),
                    Description = desc,
                    TransactionType = type,
                    Amount = amt,
                    Date = DateTime.Now,
                };

                expenses.Add(transaction);
                StorageService.SaveExpenses(expenses);
                StorageService.AddLog($"Added {type}: {desc} - {amt}€");

                if (type == TransactionType.Income)
                {
                    AnsiConsole.MarkupLine($"[green]{type} added![/]");
                }
                if (type == TransactionType.Expense)
                {
                    AnsiConsole.MarkupLine($"[red]{type} added![/]");
                }
            }

            static void ViewTransactions()
            {
                if (expenses.Count() != 0)
                {
                    Table table = new Table()
                        .Border(TableBorder.Rounded)
                        .Centered()
                        .AddColumn("ID")
                        .AddColumn("Description")
                        .AddColumn("Amount")
                        .AddColumn("Date");

                    foreach (var t in expenses)
                    {
                        if (t.TransactionType == 0)
                        {
                            table.AddRow(
                                $"{t.Id}",
                                $"{t.Description}",
                                $"[red]{t.Amount}€[/]",
                                $"{t.Date:dd/MM/yyyy}"
                            );
                        }
                        else
                        {
                            table.AddRow(
                                $"{t.Id}",
                                $"{t.Description}",
                                $"[green]{t.Amount}€[/]",
                                $"{t.Date:dd/MM/yyyy}"
                            );
                        }
                    }
                    table.AddRow("", "", "", "");
                    var showBalance = ShowBalance();
                    var showTotalIncome = ShowTotal(TransactionType.Income);
                    var showTotalExpense = ShowTotal(TransactionType.Expense);
                    table.AddRow("Total Income:", "", $"[green]{showTotalIncome}€[/]", "");
                    table.AddRow("Total Expense:", "", $"[red]{showTotalExpense}€[/]", "");
                    table.AddRow("Balance:", "", $"{showBalance}€", "");
                    AnsiConsole.Write(table);
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]No Transactions found[/]");
                }
                ;
            }

            static void DeleteTransaction()
            {
                if (expenses.Count() != 0)
                {
                    var transactionDeleteList = AnsiConsole.Prompt(
                        new MultiSelectionPrompt<Expense>()
                            .Title("Select transactions to delete")
                            .PageSize(10)
                            .NotRequired()
                            .InstructionsText("Press <space> to select, <enter> to delete")
                            .UseConverter(e =>
                                $"{e.Date:yyyy-MM-dd} | {e.TransactionType} | {e.Amount:C} | "
                                + $"{e.Description ?? "<no description>"}"
                            )
                            .AddChoices(expenses)
                    );

                    if (transactionDeleteList.Count() == 0)
                    {
                        AnsiConsole.MarkupLine("[red]Nothing selected[/]");
                    }
                    else
                    {
                        foreach (var item in transactionDeleteList)
                        {
                            expenses.Remove(item);

                            StorageService.AddLog(
                                $"Deleted {item.TransactionType}: {item.Description} - {item.Amount}€"
                            );
                        }
                        ;
                    }
                    ;

                    StorageService.SaveExpenses(expenses);
                    AnsiConsole.MarkupLine(
                        $"[green]{transactionDeleteList.Count} transaction deleted.[/]"
                    );
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]No Transactions found[/]");
                }
                ;
            }

            static decimal ShowTotal(TransactionType type)
            {
                var total = expenses.Where(e => e.TransactionType == type).Sum(e => e.Amount);
                return total;
            }

            static decimal ShowBalance()
            {
                var totalE = expenses
                    .Where(e => e.TransactionType == TransactionType.Expense)
                    .Sum(e => e.Amount);
                var totalI = expenses
                    .Where(e => e.TransactionType == TransactionType.Income)
                    .Sum(e => e.Amount);
                var balance = totalI - totalE;

                return balance;
            }

            static void ViewLog()
            {
                var logs = StorageService.LoadLog();

                if (logs.Count() == 0)
                {
                    AnsiConsole.MarkupLine("[red]No logs found[/]");
                }
                else
                {
                    Table table = new Table()
                        .Border(TableBorder.Rounded)
                        .Centered()
                        .AddColumn("ID")
                        .AddColumn("Message")
                        .AddColumn("Date");

                    foreach (var log in logs)
                    {
                        table.AddRow($"{log.Id}", $"{log.Message}", $"{log.Date:dd/MM/yyyy}");
                    }
                    AnsiConsole.Write(table);
                }
            }
            ;

            static void AddTransactionMenu()
            {
                List<string> transactionchoice = ["Add Income", "Add Expense"];
                string transactionMenuChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\nSelect a Transactiontype")
                        .AddChoices(transactionchoice)
                );
                switch (transactionMenuChoice)
                {
                    case "Add Income":
                        AddTransaction(TransactionType.Income);
                        break;
                    case "Add Expense":
                        AddTransaction(TransactionType.Expense);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
}
