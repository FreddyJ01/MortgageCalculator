namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        // Variables
        string? inputString;
        double homePrice;
        double downPayment;
        int termLength;
        double interestRate;
        int paymentFrequnecy;
        double hoaFees;
        double annualIncome;
        double taxRate = 2500;
        double originationFee;
        double loanAmount;
        double payments;

        // Methods
        void UserInterface()
        {
            // Prep and Title
            Console.Clear();
            System.Console.WriteLine("Mortgage Calculator\n");

            // homePrice
            System.Console.Write("Home Price: $");
            inputString = Console.ReadLine();
            double.TryParse(inputString, out homePrice);
            System.Console.WriteLine($"Home Price: ${homePrice}\n");

            // downPayment
            System.Console.Write("Down Payment: $");
            inputString = Console.ReadLine();
            double.TryParse(inputString, out downPayment);
            System.Console.WriteLine($"Down Payment: ${downPayment}\n");

            // termLength
            System.Console.Write("Term Length (Yrs): ");
            inputString = Console.ReadLine();
            int.TryParse(inputString, out termLength);
            System.Console.WriteLine($"Term Length: {termLength} Years\n");

            // interestRate
            System.Console.Write("Interest Rate (%): ");
            inputString = Console.ReadLine();
            double.TryParse(inputString, out interestRate);
            interestRate /= 100;
            System.Console.WriteLine($"Interest Rate: {interestRate}%\n");

            // paymentFrequency - I'd rather this be implied as 12.
            System.Console.Write("Payment Per Year: ");
            inputString = Console.ReadLine();
            int.TryParse(inputString, out paymentFrequnecy);
            System.Console.WriteLine($"Payment Frequency: {paymentFrequnecy} a Year\n");

            // hoaFees
            System.Console.Write("Annual HOA Fees: $");
            inputString = Console.ReadLine();
            double.TryParse(inputString, out hoaFees);
            System.Console.WriteLine($"HOA Fees: {hoaFees}\n");

            // annualIncome
            System.Console.Write("Annual Income: $");
            inputString = Console.ReadLine();
            double.TryParse(inputString, out annualIncome);
            System.Console.WriteLine($"Annual Income: {annualIncome}\n");

            originationFee = homePrice * 0.01;
        }

        double LoanCalculator(double homePrice, double taxRate, double downPayment, double originationFee)
        {
            return ((homePrice + taxRate - downPayment) + originationFee);
        }

        double PaymentCalculator(double loanAmount, double interestRate, int paymentFrequency, int termLength, double hoaFees, double homePrice)
        {
            double payment = (loanAmount * (interestRate / paymentFrequency) * Math.Pow(1 + interestRate / paymentFrequency, paymentFrequency * termLength)) / (Math.Pow(1 + interestRate / paymentFrequency, paymentFrequency * termLength - 1));

            payment += hoaFees;
            payment += (homePrice * 0.0125);
            return payment;
        }

        UserInterface();
        loanAmount = LoanCalculator(homePrice, taxRate, downPayment, originationFee);
        System.Console.WriteLine($"Loan Amount: ${loanAmount}");
        payments = PaymentCalculator(loanAmount, interestRate, paymentFrequnecy, termLength, hoaFees, homePrice);
        System.Console.WriteLine($"Payments: ${payments:f2}");
        
    }
}
