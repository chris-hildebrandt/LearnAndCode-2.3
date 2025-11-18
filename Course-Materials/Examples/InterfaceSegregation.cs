// Bad example of the interface segregation principle

public interface IWorker
{
    void Work8to5();

    void Work9to5();
}

// The robot only works, it can't eat or sleep so this is violating the interface segregation principle
public class Robot : IWorker
{
    // We shouldn't need to have this method on a robot
    public void Eat()
    {
        throw new Exception("I don't eat");
    }

    public void Work()
    {
        // Work
    }

    // We shouldn't need to have this method on a robot
    public void Sleep()
    {
        throw new Exception("I don't sleep");
    }
}

public class Human : IWorker
{
    public void Eat()
    {
        // Eat
    }

    public void Work()
    {
        // Work
    }

    public void Sleep()
    {
        // Sleep
    }
}

public interface IWorker
{
    void Work();
}

public interface IEater
{
    void Eat();
}

public interface ISleeper
{
    void Sleep();
}

public class Robot : IWorker
{
    public void Work()
    {
        // Work
    }
}

public class HumanService : IWorker, IEater, ISleeper
{
    public void Work()
    {
        // Work
    }

    public void Eat()
    {
        // Eat
    }

    public void Sleep()
    {
        // Sleep
    }
}

public class HumanController
{
    private readonly IEater _eater;
    private readonly ISleeper _sleeper;

    public HumanService(IEater eater)
    {
        _eater = eater;
    }

    public void Eat()
    {
        _eater.Eat();
    }

    public void Sleep()
    {
        _sleeper.Sleep();
    }
}


public class Program
{
    public static void Main()
    {
        var robot = new Robot();
        var humanService = new HumanService();
        var humanController = new HumanController(humanService);
        var humanController;
        humanController.Eat();
        humanController.Work();
    }
}

