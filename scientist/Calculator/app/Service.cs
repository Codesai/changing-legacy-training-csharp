using Calculator.legacy;
using Calculator.model;

namespace Calculator.app;

public class Service
{
    public IEnumerable<MortgagePaymentDto> Execute(int durationInYears, double interest, decimal principal)
    {
        var answer = CreateQuestionnaire(durationInYears, interest, principal);
        var mortgagePayments = new LegacyMortgageCalculator().Calculate(answer);
        return mortgagePayments.Select(CreateMortgageDto);
    }

    private static MortgagePaymentDto CreateMortgageDto(MortgagePayment x)
    {
        return new MortgagePaymentDto
        {
            Month = x.Month,
            StartingBalance = x.StartingBalance,
            Interest = x.Interest,
            MonthlyPayment = x.MonthlyPayment,
            Principal = x.Principal,
            EndingBalance = x.EndingBalance
        };
    }

    private static Questionnaire CreateQuestionnaire(int durationInYears, double interest, decimal principal)
    {
        return new Questionnaire
        {
            DurationInYears = durationInYears,
            Interest = interest,
            Principal = principal
        };
    }
}