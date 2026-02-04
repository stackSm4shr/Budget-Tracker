// class Program
// {
//     static List<Expense> expenses = StorageService.LoadExpenses();

//     static void Main()
//     {
//         while (true)
//         {
//             Console.Clear();
//             DrawMenu();

//             Console.Write("\nSelect an option: ");
//             var input = Console.ReadLine();

//             switch (input)
//             {
//                 case "1":
//                     AddTransaction(TransactionType.Expense);
//                     break;
//                 case "2":
//                     AddTransaction(TransactionType.Income);
//                     break;
//                 case "3":
//                     ViewTransactions();
//                     break;
//                 case "4":
//                     DeleteTransaction();
//                     break;
//                 case "5":
//                     ShowTotal(TransactionType.Expense);
//                     break;
//                 case "6":
//                     ShowTotal(TransactionType.Income);
//                     break;
//                 case "7":
//                     ShowBalance();
//                     break;
//                 case "8":
//                     return;
//                 default:
//                     Console.WriteLine("Invalid option");
//                     break;
//             }

//             Console.WriteLine("Press any key to continue...");
//             Console.ReadKey();
//         }
//     }

//     static void DrawMenu()
//     {
//         Console.Write("|<====");
//         Console.ForegroundColor = ConsoleColor.Yellow;
//         Console.Write(" Budget Tracker ");
//         Console.ForegroundColor = ConsoleColor.White;
//         Console.WriteLine("====>|");

//         Console.Write("| 1. | Add ");
//         Console.ForegroundColor = ConsoleColor.Red;
//         Console.Write("Expense");
//         Console.ForegroundColor = ConsoleColor.White;
//         Console.WriteLine("         |");

//         Console.Write("| 2. | Add ");
//         Console.ForegroundColor = ConsoleColor.Green;
//         Console.Write("Income");
//         Console.ForegroundColor = ConsoleColor.White;
//         Console.WriteLine("          |");

//         Console.WriteLine("| 3. | View Transactions   |");
//         Console.WriteLine("| 4. | Delete Transaction  |");

//         Console.Write("| 5. | Total ");
//         Console.ForegroundColor = ConsoleColor.Red;
//         Console.Write("Expense");
//         Console.ForegroundColor = ConsoleColor.White;
//         Console.WriteLine("       |");

//         Console.Write("| 6. | Total ");
//         Console.ForegroundColor = ConsoleColor.Green;
//         Console.Write("Income");
//         Console.ForegroundColor = ConsoleColor.White;
//         Console.WriteLine("        |");

//         Console.WriteLine("| 7. | Balance             |");
//         Console.WriteLine("| 8. | Exit                |");
//         Console.WriteLine("|<====   #**++++**#   ====>|");
//     }

//     static void AddTransaction(TransactionType type)
//     {
//         Console.Write("Description: ");
//         var desc = Console.ReadLine();

//         Console.Write("Amount: ");
//         decimal amt = decimal.TryParse(Console.ReadLine(), out amt) ? amt : 0;

//         var transaction = new Expense
//         {
//             Id = Guid.NewGuid(),
//             Description = desc,
//             TransactionType = type,
//             Amount = amt,
//             Date = DateTime.Now,
//         };

//         expenses.Add(transaction);
//         StorageService.SaveExpenses(expenses);
//         StorageService.AddLog($"Added {type}: {desc} - {amt}€");

//         Console.WriteLine($"{type} added!");
//     }

//     static void ViewTransactions()
//     {
//         Console.WriteLine("\n------------ Transactions ------------");
//         Console.WriteLine(
//             "|                  ID                  |       Description  |   Amount  |   Date   |"
//         );

//         foreach (var t in expenses)
//         {
//             Console.ForegroundColor =
//                 t.TransactionType == TransactionType.Expense
//                     ? ConsoleColor.Red
//                     : ConsoleColor.Green;

//             Console.WriteLine(
//                 $"| {t.Id} | {t.Description, -20} | {t.Amount, 8}€ | {t.Date:dd/MM/yyyy} |"
//             );
//             Console.ForegroundColor = ConsoleColor.White;
//         }
//     }

//     static void DeleteTransaction()
//     {
//         ViewTransactions();
//         Console.Write("Enter ID to delete: ");

//         if (Guid.TryParse(Console.ReadLine(), out Guid id))
//         {
//             var item = expenses.FirstOrDefault(t => t.Id == id);
//             if (item != null)
//             {
//                 expenses.Remove(item);
//                 StorageService.SaveExpenses(expenses);
//                 StorageService.AddLog(
//                     $"Deleted {item.TransactionType}: {item.Description} - {item.Amount}€"
//                 );
//                 Console.WriteLine("Deleted!");
//             }
//             else
//             {
//                 Console.WriteLine("ID not found.");
//             }
//         }
//         else
//         {
//             Console.WriteLine("Invalid ID format.");
//         }
//     }

//     static void ShowTotal(TransactionType type)
//     {
//         var total = expenses.Where(e => e.TransactionType == type).Sum(e => e.Amount);

//         Console.ForegroundColor =
//             type == TransactionType.Expense ? ConsoleColor.Red : ConsoleColor.Green;

//         Console.WriteLine($"\nTotal {type}: {total}€");
//         Console.ForegroundColor = ConsoleColor.White;
//     }

//     static void ShowBalance()
//     {
//         var totalE = expenses
//             .Where(e => e.TransactionType == TransactionType.Expense)
//             .Sum(e => e.Amount);
//         var totalI = expenses
//             .Where(e => e.TransactionType == TransactionType.Income)
//             .Sum(e => e.Amount);
//         var balance = totalI - totalE;

//         Console.WriteLine($"\nBalance: {balance}€");
//     }
// }
