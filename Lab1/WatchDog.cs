public class WatchDog {
    private readonly int sleepTime;
    private readonly List<WatchDogAwareThread> threads;

    public WatchDog(int sleepTime, List<WatchDogAwareThread> threads) {
        this.sleepTime = sleepTime;
        this.threads = threads;
    }

    public void Run() {
        StartThreads();
        try {
            Thread.Sleep(sleepTime * 1000);
            TerminateThreads();
        } catch (ThreadInterruptedException) {
            Console.Error.WriteLine("Watchdog: Application was interrupted! Stopping all threads...");
            TerminateThreads();
            Thread.CurrentThread.Interrupt();
        }
    }

    private void TerminateThreads() {
        foreach (var thread in threads) {
            thread.Terminate();
        }
    }

    private void StartThreads() {
        foreach (var thread in threads) {
            new Thread(new ThreadStart(thread.Run)).Start();
        }
    }
}
