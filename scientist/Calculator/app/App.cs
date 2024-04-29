using Calculator.scientist;
using GitHub;

namespace Calculator.app;

public abstract class App
{
    public static void Main(string[] args)
    {
        Scientist.ResultPublisher = new FireAndForgetResultPublisher(
            new MortgagePaymentsResultPublisher(CreatePathInProject("scientist-result.txt")));
        
        var service = new Service();
        var examples = GenerateSomeExamples();
        foreach (var example in examples)
        {
            var result = service.Execute(example.Item1, example.Item2, example.Item3);
            PrintResult(result);
        }
    }

    private static void PrintResult(IEnumerable<MortgagePaymentDto> result)
    {
        Console.WriteLine("== Your Mortgage Calculations ==");
        result.ToList().ForEach(x =>
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Month: " + x.Month);
            Console.WriteLine("Principal Monthly: " + x.Principal);
            Console.WriteLine("Starting Balance: " + x.StartingBalance);
            Console.WriteLine("Ending Balance " + x.EndingBalance);
            Console.WriteLine("Monthly Payment: " + x.MonthlyPayment);
            Console.WriteLine("Monthly Interes: " + x.Interest);
        });
    }

    private static Tuple<int, double, decimal>[] GenerateSomeExamples()
    {
        return new[] {
            Tuple.Create(1, 4d, 3000m),
            Tuple.Create(1, 3d, 100000m),
            Tuple.Create(2, 2d, 60000m),
            Tuple.Create(3, 2d, 150000m),
            Tuple.Create(5, 3d, 150000m)
        };
    }

    private static string CreatePathInProject(string filename)
    {
        var path = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,"..","..","..",filename);
        return Path.GetFullPath(path);
    }
}