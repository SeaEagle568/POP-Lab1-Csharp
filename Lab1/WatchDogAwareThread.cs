public abstract class WatchDogAwareThread {
    private volatile bool running = true;

    public void Terminate() {
        running = false;
    }

    protected bool IsRunning() {
        return running;
    }

    public abstract void Run();
}