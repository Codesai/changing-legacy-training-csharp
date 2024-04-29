using Calculator.model;

namespace Calculator.legacy;

public class LegacyMortgageCalculator : IMortgageCalculator
{
    public List<MortgagePayment> Calculate(Questionnaire answer)
    {
        return Calculations.MortgageCalculations(answer);
    }
}