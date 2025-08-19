# 🏠 Mortgage Calculator

![.NET version](https://img.shields.io/badge/.NET-8.0-blue)
![Language](https://img.shields.io/badge/language-C%23-brightgreen)
![Status](https://img.shields.io/badge/status-Active-success)

A powerful, interactive mortgage calculator console app built with C# and .NET 8.0. Instantly analyze home loan scenarios, monthly payments, and approval status with clear, actionable results.

---

## ✨ Features

- **Comprehensive Loan Calculations:**  
  Calculates principal, interest, origination fees, closing costs and equity.
- **Monthly Payment Breakdown:**  
  Shows principal & interest, property tax, insurance, HOA fees, and loan insurance.
- **Dynamic Approval Status:**  
  Evaluates payment-to-income ratio for loan approval decision.
- **Equity Analysis:**  
  Initial equity value and percentage, insurance requirement based on equity.
- **User-Friendly Console Experience:**  
  Prompts for all necessary details, displays results in a clear, organized format.

---

## 🚀 Quick Start

1. **Clone the repository:**
   ```bash
   git clone https://github.com/FreddyJ01/MortgageCalculator.git
   cd MortgageCalculator/ConsoleApp1
   ```

2. **Build and run (requires [.NET 8 SDK](https://dotnet.microsoft.com/download)):**
   ```bash
   dotnet run
   ```

3. **Follow the prompts:**
   - Enter purchase price, market value, down payment
   - Specify loan term (15 or 30 years), interest rate, income, HOA fees
   - View a detailed breakdown of your monthly payment and approval analysis!

---

## 📋 Sample Output

```
=== Mortgage Loan Calculator ===

Purchase Price: $400,000.00
Market Value: $415,000.00
Down Payment: $40,000.00
Total Loan Amount: $362,500.00
Loan Term: 30 years
Interest Rate: 6.50%

--- EQUITY ANALYSIS ---
Initial Equity Value: $40,000.00
Initial Equity Percentage: 9.64%

--- MONTHLY PAYMENT BREAKDOWN ---
Base Payment (Principal & Interest): $2,292.53
Property Tax (monthly): $400.00
Homeowner's Insurance (monthly): $70.00
HOA Fees (monthly): $50.00
Loan Insurance (monthly): $80.00
  * Required because equity < 10% of market value

TOTAL MONTHLY PAYMENT: $2,892.53

--- LOAN APPROVAL ANALYSIS ---
Monthly Income: $7,500.00
Payment to Income Ratio: 0.39
Loan Approval Status: Approved
```

---

## 🛠️ How It Works

- **LoanInputData:** Collects all user data for calculation.
- **LoanCalculator:** Core logic for all mortgage math, insurance, equity, and approval.
- **LoanResults:** Outputs all calculated results for display.

See [`Program.cs`](ConsoleApp1/Program.cs) for the main logic and structure.

---

## 🧑‍💻 Contributing

PRs welcome! Open issues or suggestions to improve features and usability.

---

## 📄 License

MIT License © FreddyJ01

---

> **Built for MSSA TechDev Projects.**
