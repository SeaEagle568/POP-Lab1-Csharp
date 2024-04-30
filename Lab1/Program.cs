using System;
using System.Collections.Generic;
using System.Threading;

class Program {
    private static readonly string ERR_INPUT_MISMATCH = "Found invalid value in input - expected integer 0 < x <= 30.\nPlease try again:";

    static void Main(string[] args) {
        Console.WriteLine("Lab 1, made by Oleksiienko Pavlo");
        Console.WriteLine("Threads are calculating the arithmetical progression with a given delta.");
        int threadsCount = ExtractFromSTDIN("number of threads");
        int sleepS = ExtractFromSTDIN("wait time in seconds");
        int progressionDelta = ExtractFromSTDIN("integer progression delta");

        List<WatchDogAwareThread> threads = new List<WatchDogAwareThread>();
        for (int i = 1; i <= threadsCount; i++) {
            threads.Add(new CalculatorThread(progressionDelta, "Calculator " + i));
        }
        WatchDog watchDog = new WatchDog(sleepS, threads);
        Console.WriteLine("Starting runners");
        new Thread(new ThreadStart(watchDog.Run)).Start();
    }

    private static int ExtractFromSTDIN(string ask) {
        Console.WriteLine($"Please enter {ask}:");
        while (true) {
            if (int.TryParse(Console.ReadLine(), out int value)) {
                if (value < 1 || value > 30) {
                    Console.Error.WriteLine(ERR_INPUT_MISMATCH);
                    continue;
                }
                return value;
            } else {
                Console.Error.WriteLine(ERR_INPUT_MISMATCH);
            }
        }
    }
}
