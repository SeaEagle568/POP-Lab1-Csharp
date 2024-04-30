public class CalculatorThread : WatchDogAwareThread {
    private readonly int delta;
    private readonly string name;

    public CalculatorThread(int delta, string name) {
        this.delta = delta;
        this.name = name;
    }

    public override void Run() {
        long sum = 0;
        while (IsRunning()) {
            sum += delta;
        }
        Console.WriteLine($"{name} finished with result: {sum}");
    }
}