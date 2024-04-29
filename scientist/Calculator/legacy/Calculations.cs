using Calculator.model;

namespace Calculator.legacy;

public static class Calculations
{
    public static List<MortgagePayment> MortgageCalculations(Questionnaire answer)
    {
        Thread.Sleep(500); 
        List<MortgagePayment> output = new();

        var durationInMonths = CalculateTotalMonths(answer.DurationInYears);
        decimal monthlyPayment = MonthlyPayment(answer.Principal, answer.Interest, durationInMonths, false);
        decimal displayMonthlyPayment = MonthlyPayment(answer.Principal, answer.Interest, durationInMonths, true);
        int calculateMonth = 0;
        for (int i = durationInMonths; i > 0; i--)
        {
            calculateMonth++;

            decimal monthlyInterest = MonthlyInterest(answer.Principal, answer.Interest);
            decimal monthlyPrinipalPayment = monthlyPayment - monthlyInterest;
            decimal startingBalance = answer.Principal;
            answer.Principal += monthlyInterest;
            answer.Principal -= monthlyPayment;
            decimal displayStartingBalance = Math.Round(startingBalance, 2);
            displayMonthlyPayment = Math.Round(monthlyPayment, 2);
            decimal endingBalance = Math.Round(answer.Principal, 2);
            decimal displayInterest = Math.Round(monthlyInterest, 2);
            decimal displayMonthlyPrincipal = Math.Round(monthlyPrinipalPayment, 2);
            output.Add(new MortgagePayment
            {
                Month = calculateMonth,
                StartingBalance = displayStartingBalance,
                Interest = displayInterest,
                MonthlyPayment = displayMonthlyPayment,
                Principal = displayMonthlyPrincipal,
                EndingBalance = endingBalance
            });
        }

        return output;
    }

    private static decimal MonthlyPayment(decimal principal, double interest, int duration, bool isDisplayValue)
    {
        decimal output = 0;

        double montlyInterestRate = interest.MonthlyInterestRate();
        decimal mortgageMagic = (decimal)(1 - Math.Pow(1 + montlyInterestRate, -duration));

        output = principal * ((decimal)montlyInterestRate) / mortgageMagic;
        if (isDisplayValue)
        {
            output = Math.Round(output, 2);
        }

        return output;
    }

    private static decimal MonthlyInterest(decimal principal, double interest)
    {
        decimal output = 0;

        output = principal * (decimal)MonthlyInterestRate(interest);

        return output;
    }


    private static double MonthlyInterestRate(this double interest)
    {
        return interest / 100 / 12;
    }

    private static int CalculateTotalMonths(this int years)
    {
        return years * 12;
    }
}