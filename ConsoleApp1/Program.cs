using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Mortgage Loan Calculator ===\n");

            try
            {
                // Get user inputs
                var loanData = GetLoanInputs();
                
                // Calculate loan details
                var calculator = new LoanCalculator();
                var results = calculator.CalculateLoan(loanData);
                
                // Display results
                DisplayResults(results, loanData);
                
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        static LoanInputData GetLoanInputs()
        {
            var data = new LoanInputData();

            Console.Write("Enter home purchase price: $");
            data.PurchasePrice = GetDecimalInput();

            Console.Write("Enter current market value of home: $");
            data.MarketValue = GetDecimalInput();

            Console.Write("Enter down payment amount: $");
            data.DownPayment = GetDecimalInput();

            Console.Write("Enter loan term (15 or 30 years): ");
            data.LoanTermYears = GetLoanTerm();

            Console.Write("Enter annual interest rate (%): ");
            data.InterestRate = GetDecimalInput() / 100;

            Console.Write("Enter buyer's monthly income: $");
            data.MonthlyIncome = GetDecimalInput();

            Console.Write("Enter yearly HOA fees (0 if none): $");
            data.YearlyHOAFees = GetDecimalInput();

            return data;
        }

        static decimal GetDecimalInput()
        {
            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out decimal value) && value >= 0)
                    return value;
                Console.Write("Please enter a valid positive number: ");
            }
        }

        static int GetLoanTerm()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int term) && (term == 15 || term == 30))
                    return term;
                Console.Write("Please enter either 15 or 30: ");
            }
        }

        static void DisplayResults(LoanResults results, LoanInputData input)
        {
            Console.WriteLine("\n=== LOAN CALCULATION RESULTS ===\n");
            
            Console.WriteLine($"Purchase Price: ${input.PurchasePrice:N2}");
            Console.WriteLine($"Market Value: ${input.MarketValue:N2}");
            Console.WriteLine($"Down Payment: ${input.DownPayment:N2}");
            Console.WriteLine($"Total Loan Amount: ${results.TotalLoanAmount:N2}");
            Console.WriteLine($"Loan Term: {input.LoanTermYears} years");
            Console.WriteLine($"Interest Rate: {input.InterestRate:P2}");
            
            Console.WriteLine("\n--- EQUITY ANALYSIS ---");
            Console.WriteLine($"Initial Equity Value: ${results.EquityValue:N2}");
            Console.WriteLine($"Initial Equity Percentage: {results.EquityPercentage:P2}");
            
            Console.WriteLine("\n--- MONTHLY PAYMENT BREAKDOWN ---");
            Console.WriteLine($"Base Payment (Principal & Interest): ${results.BaseMonthlyPayment:N2}");
            Console.WriteLine($"Property Tax (monthly): ${results.MonthlyPropertyTax:N2}");
            Console.WriteLine($"Homeowner's Insurance (monthly): ${results.MonthlyInsurance:N2}");
            Console.WriteLine($"HOA Fees (monthly): ${results.MonthlyHOA:N2}");
            
            if (results.RequiresLoanInsurance)
            {
                Console.WriteLine($"Loan Insurance (monthly): ${results.MonthlyLoanInsurance:N2}");
                Console.WriteLine($"  * Required because equity < 10% of market value");
            }
            else
            {
                Console.WriteLine("Loan Insurance: Not required");
            }
            
            Console.WriteLine($"\nTOTAL MONTHLY PAYMENT: ${results.TotalMonthlyPayment:N2}");
            
            Console.WriteLine("\n--- LOAN APPROVAL ANALYSIS ---");
            Console.WriteLine($"Monthly Income: ${input.MonthlyIncome:N2}");
            Console.WriteLine($"Payment to Income Ratio: {results.PaymentToIncomeRatio:P2}");
            
            if (results.IsApproved)
            {
                Console.WriteLine("✓ LOAN APPROVED - Payment is less than 25% of monthly income");
            }
            else
            {
                Console.WriteLine("✗ LOAN DENIED - Payment is 25% or more of monthly income");
                Console.WriteLine("\nRECOMMENDATIONS:");
                Console.WriteLine("• Consider placing more money down to reduce the loan amount");
                Console.WriteLine("• Look at buying a more affordable home");
                Console.WriteLine($"• Target a monthly payment under ${input.MonthlyIncome * 0.25m:N2} (25% of income)");
            }
        }
    }

    public class LoanInputData
    {
        public decimal PurchasePrice { get; set; }
        public decimal MarketValue { get; set; }
        public decimal DownPayment { get; set; }
        public int LoanTermYears { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal YearlyHOAFees { get; set; }
    }

    public class LoanResults
    {
        public decimal InitialLoanAmount { get; set; }
        public decimal TotalLoanAmount { get; set; }
        public decimal BaseMonthlyPayment { get; set; }
        public decimal MonthlyPropertyTax { get; set; }
        public decimal MonthlyInsurance { get; set; }
        public decimal MonthlyHOA { get; set; }
        public decimal MonthlyLoanInsurance { get; set; }
        public decimal TotalMonthlyPayment { get; set; }
        public decimal EquityValue { get; set; }
        public decimal EquityPercentage { get; set; }
        public bool RequiresLoanInsurance { get; set; }
        public decimal PaymentToIncomeRatio { get; set; }
        public bool IsApproved { get; set; }
    }

    public class LoanCalculator
    {
        private const decimal ORIGINATION_FEE_RATE = 0.01m; // 1%
        private const decimal CLOSING_COSTS = 2500m;
        private const decimal PROPERTY_TAX_RATE = 0.0125m; // 1.25% yearly
        private const decimal INSURANCE_RATE = 0.0075m; // 0.75% yearly
        private const decimal LOAN_INSURANCE_RATE = 0.01m; // 1% yearly
        private const decimal MIN_EQUITY_PERCENTAGE = 0.10m; // 10%
        private const decimal MAX_PAYMENT_TO_INCOME_RATIO = 0.25m; // 25%

        public LoanResults CalculateLoan(LoanInputData input)
        {
            var results = new LoanResults();

            // Calculate initial loan amount (purchase price - down payment)
            results.InitialLoanAmount = input.PurchasePrice - input.DownPayment;

            // Calculate total loan amount (initial + origination fee + closing costs)
            decimal originationFee = results.InitialLoanAmount * ORIGINATION_FEE_RATE;
            results.TotalLoanAmount = results.InitialLoanAmount + originationFee + CLOSING_COSTS;

            // Calculate equity
            results.EquityValue = input.DownPayment;
            results.EquityPercentage = input.MarketValue > 0 ? results.EquityValue / input.MarketValue : 0;

            // Check if loan insurance is required
            results.RequiresLoanInsurance = results.EquityPercentage < MIN_EQUITY_PERCENTAGE;

            // Calculate base monthly payment (principal and interest)
            results.BaseMonthlyPayment = CalculateMonthlyPayment(
                results.TotalLoanAmount, 
                input.InterestRate, 
                input.LoanTermYears);

            // Calculate monthly escrow amounts
            results.MonthlyPropertyTax = (input.MarketValue * PROPERTY_TAX_RATE) / 12;
            results.MonthlyInsurance = (input.MarketValue * INSURANCE_RATE) / 12;

            // Calculate monthly HOA
            results.MonthlyHOA = input.YearlyHOAFees / 12;

            // Calculate monthly loan insurance if required
            if (results.RequiresLoanInsurance)
            {
                results.MonthlyLoanInsurance = (results.InitialLoanAmount * LOAN_INSURANCE_RATE) / 12;
            }

            // Calculate total monthly payment
            results.TotalMonthlyPayment = results.BaseMonthlyPayment +
                                       results.MonthlyPropertyTax +
                                       results.MonthlyInsurance +
                                       results.MonthlyHOA +
                                       results.MonthlyLoanInsurance;

            // Calculate payment to income ratio and approval
            results.PaymentToIncomeRatio = input.MonthlyIncome > 0 ? 
                results.TotalMonthlyPayment / input.MonthlyIncome : 0;
            results.IsApproved = results.PaymentToIncomeRatio < MAX_PAYMENT_TO_INCOME_RATIO;

            return results;
        }

        private decimal CalculateMonthlyPayment(decimal loanAmount, decimal annualRate, int years)
        {
            if (annualRate == 0)
                return loanAmount / (years * 12);

            decimal monthlyRate = annualRate / 12;
            int numberOfPayments = years * 12;
            
            // Standard mortgage payment formula: M = P * [r(1+r)^n] / [(1+r)^n - 1]
            decimal factor = (decimal)Math.Pow((double)(1 + monthlyRate), numberOfPayments);
            return loanAmount * (monthlyRate * factor) / (factor - 1);
        }
    }
}