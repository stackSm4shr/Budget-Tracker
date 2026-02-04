# 💰 Budget Tracker (Console App)

A simple **C# console-based budget tracker** built with **Spectre.Console**.  
Track your income and expenses, view totals and balance, delete transactions, and keep an activity log — all from a clean, interactive terminal UI.

---

## ✨ Features

- ➕ Add **income** and **expense** transactions  
- 📋 View all transactions in a styled table  
- 🧮 Automatic calculation of:
  - Total income
  - Total expenses
  - Current balance
- ❌ Delete one or multiple transactions
- 📝 View an activity log (adds & deletes)
- 🎨 Colorful, user-friendly terminal UI using **Spectre.Console**
- 💾 Persistent storage via local files

---

## 🛠️ Built With

- **C# / .NET**
- **Spectre.Console**
- LINQ
- File-based persistence

---

## 📸 Preview

                         ____                _                  _   
                        | __ )   _   _    __| |   __ _    ___  | |_ 
                        |  _ \  | | | |  / _` |  / _` |  / _ \ | __|
                        | |_) | | |_| | | (_| | | (_| | |  __/ | |_ 
                        |____/   \__,_|  \__,_|  \__, |  \___|  \__|
                                                 |___/              
                      _____                         _                  
                     |_   _|  _ __    __ _    ___  | | __   ___   _ __ 
                       | |   | '__|  / _` |  / __| | |/ /  / _ \ | '__|
                       | |   | |    | (_| | | (__  |   <  |  __/ | |   
                       |_|   |_|     \__,_|  \___| |_|\_\  \___| |_|   

    What do you want to do?
                       
    > Add Transaction      
      View Transactions    
      Delete Transaction   
      View Log             
      Exit                 

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/) (6.0 or newer recommended)

### Installation & Run

```bash
git clone https://github.com/your-username/budget-tracker.git
cd budget-tracker
dotnet restore
dotnet run
```
## 📖 How It Works

### Main Menu

- Add Transaction

  - Choose between Income or Expense

  - Enter a description and amount

- View Transactions

  - Displays all transactions in a table

  - Shows total income, total expense, and balance

- Delete Transaction

  - Select one or multiple transactions to delete

- View Log

  - Displays a log of added and deleted transactions

- Exit

  - Closes the application

### Transaction Display

- Income is shown in green

- Expenses are shown in red

## 📂 Project Structure
```bash
BudgetTracker/
│
├── Program.cs
├── Expense.cs
├── TransactionType.cs
├── StorageService.cs
└── data/
    ├── expenses.json
    └── log.json
```
