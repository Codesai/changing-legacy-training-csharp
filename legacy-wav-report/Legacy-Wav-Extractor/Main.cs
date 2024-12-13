using System;
using System.IO;
using System.Linq;

namespace Legacy_Wav_Extractor;

public static class App 
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            throw new ArgumentException("You must provide an directory");
        }
        
        Console.WriteLine("Executing WavExtractor...");
        Execute(args);
        Console.WriteLine("Finish WavExtractor");
    }


    private static void Execute(string[] args)
    {
        var wavExtractor = WavExtractorFactory.Get();
        var optional = args.ToList().GetRange(1, args.Length - 1);
        var files = Directory
            .GetFiles(args[0])
            .ToList();
        wavExtractor.Execute(files, optional);
    }
}