# **Design Patterns - Examples**

This README showcases examples of implementing design patterns in software development projects to improve code quality and maintainability. The purpose of this example is to demonstrate the positive impact of design patterns and their alignment with the Quality Manifesto's focus on well-structured code.

## **Table of Contents**

- **[Singleton Pattern](#singleton-pattern)**
- **[Strategy Pattern](#strategy-pattern)**
- **[Observer Pattern](#observer-pattern)**
- **[Factory Method Pattern](#factory-method-pattern)**
- **[Decorator Pattern](#decorator-pattern)**

## **Singleton Pattern**

The Singleton pattern ensures that a class has only one instance and provides a global point of access to it. This pattern can be useful for controlling access to shared resources, such as logging or configuration objects.

### **Before**

```cs
public class Logger
{
    public void Log(string message)
    {
        // Log message to a file or console
    }
}

public class Application
{
    public void Run()
    {
        Logger logger = new Logger();
        logger.Log("Application started.");
        // ...
    }
}

```

### **After**

```cs
public class Logger
{
    private static Logger _instance;

    private Logger() { }

    public static Logger Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }
            return _instance;
        }
    }

    public void Log(string message)
    {
        // Log message to a file or console
    }
}

public class Application
{
    public void Run()
    {
        Logger.Instance.Log("Application started.");
        // ...
    }
}

```

## **Strategy Pattern**

The Strategy pattern allows you to define a family of algorithms, encapsulate each one, and make them interchangeable. This pattern can help you decouple the algorithm from the client that uses it and promote the use of composition over inheritance.

### **Before**

```cs
public class ShoppingCart
{
    public double CalculateShippingCost()
    {
        // Different shipping calculations depending on the shipping method
    }
}

```

### **After**

```cs
public interface IShippingStrategy
{
    double CalculateShippingCost(Order order);
}

public class FedExShippingStrategy : IShippingStrategy
{
    public double CalculateShippingCost(Order order)
    {
        // Calculate shipping cost using FedEx rates
    }
}

public class UPSShippingStrategy : IShippingStrategy
{
    public double CalculateShippingCost(Order order)
    {
        // Calculate shipping cost using UPS rates
    }
}

public class ShoppingCart
{
    private IShippingStrategy _shippingStrategy;

    public ShoppingCart(IShippingStrategy shippingStrategy)
    {
        _shippingStrategy = shippingStrategy;
    }

    public double CalculateShippingCost()
    {
        return _shippingStrategy.CalculateShippingCost(this);
    }
}

```

## **Observer Pattern**

The Observer pattern defines a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically. This pattern can be useful when dealing with events and notifications in an application.

### **Before**

```cs
public class WeatherData
{
    public void MeasurementsChanged()
    {
        // Code to update display components directly
    }
}

```

### **After**

```cs
public interface IObserver
{
    void Update(float temperature, float humidity, float pressure);
}

public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
}

public class WeatherData : ISubject
{
    private List<IObserver> _observers;
    // other instance variables

    public void RegisterObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

public void RemoveObserver(IObserver observer)
{
	_observers.Remove(observer);
}

public void NotifyObservers()
{
	foreach (IObserver observer in _observers)
	{
		observer.Update(/temperature, humidity, pressure/);
	}
}

public void MeasurementsChanged()
{
	NotifyObservers();
}

// other methods
}

public class CurrentConditionsDisplay : IObserver
{
	public void Update(float temperature, float humidity, float pressure)
	{
		// Update display with the new weather data
	}
}

public class WeatherApp
{
	public void Run()
	{
		WeatherData weatherData = new WeatherData();
		CurrentConditionsDisplay currentConditionsDisplay = new CurrentConditionsDisplay();
		
		weatherData.RegisterObserver(currentConditionsDisplay);
		
		// Simulate new weather measurements
		weatherData.MeasurementsChanged();
	}
}
```

## **Factory Method Pattern**

The Factory Method pattern provides an interface for creating objects in a superclass, but allows subclasses to alter the type of objects that will be created. This pattern can help you create objects without exposing the creation logic to the client and refer to newly created objects using a common interface.

### **Before**

```cs
public class DocumentReader
{
    public void ReadDocument(string filePath)
    {
        if (filePath.EndsWith(".pdf"))
        {
            // Read PDF document
        }
        else if (filePath.EndsWith(".docx"))
        {
            // Read Word document
        }
    }
}

```

### **After**

```cs
public interface IDocumentReader
{
    void ReadDocument(string filePath);
}

public class PdfReader : IDocumentReader
{
    public void ReadDocument(string filePath)
    {
        // Read PDF document
    }
}

public class WordReader : IDocumentReader
{
    public void ReadDocument(string filePath)
    {
        // Read Word document
    }
}

public static class DocumentReaderFactory
{
    public static IDocumentReader CreateReader(string filePath)
    {
        if (filePath.EndsWith(".pdf"))
        {
            return new PdfReader();
        }
        else if (filePath.EndsWith(".docx"))
        {
            return new WordReader();
        }
        else
        {
            throw new NotSupportedException("File format not supported");
        }
    }
}

public class Application
{
    public void Run(string filePath)
    {
        IDocumentReader reader = DocumentReaderFactory.CreateReader(filePath);
        reader.ReadDocument(filePath);
    }
}

```

In the refactored version using the Factory Method pattern, we created an **`IDocumentReader`** interface and concrete implementations for reading PDF and Word documents. Instead of having conditional statements within the **`ReadDocument`** method in the **`DocumentReader`** class, we moved the creation logic to a separate **`DocumentReaderFactory`**. This approach allows for better separation of concerns and makes it easier to add support for new document formats in the future.

## **Decorator Pattern**

The Decorator pattern allows you to dynamically attach new responsibilities to objects without modifying their existing behavior. This pattern is useful when you want to extend the functionality of a class but inheritance is not the best solution, as it may lead to a large number of subclasses.

### **Before**

```cs
public interface ICoffee
{
    string GetDescription();
    double GetCost();
}

public class Espresso : ICoffee
{
    public string GetDescription()
    {
        return "Espresso";
    }

    public double GetCost()
    {
        return 1.99;
    }
}

```

### **After**

```cs
public interface ICoffee
{
    string GetDescription();
    double GetCost();
}

public class Espresso : ICoffee
{
    public string GetDescription()
    {
        return "Espresso";
    }

    public double GetCost()
    {
        return 1.99;
    }
}

public abstract class CoffeeDecorator : ICoffee
{
    protected ICoffee _coffee;

    public CoffeeDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }

    public abstract string GetDescription();
    public abstract double GetCost();
}

public class Milk : CoffeeDecorator
{
    public Milk(ICoffee coffee) : base(coffee) { }

    public override string GetDescription()
    {
        return _coffee.GetDescription() + ", Milk";
    }

    public override double GetCost()
    {
        return _coffee.GetCost() + 0.30;
    }
}

public class WhippedCream : CoffeeDecorator
{
    public WhippedCream(ICoffee coffee) : base(coffee) { }

    public override string GetDescription()
    {
        return _coffee.GetDescription() + ", Whipped Cream";
    }

    public override double GetCost()
    {
        return _coffee.GetCost() + 0.50;
    }
}

```

In the refactored version using the Decorator pattern, we have the **`ICoffee`** interface which has the base methods **`GetDescription`** and **`GetCost`**. The **`Espresso`** class implements the **`ICoffee`** interface. To add new functionalities, we create a **`CoffeeDecorator`** abstract class that also implements the **`ICoffee`** interface and maintains a reference to the **`ICoffee`** object it is decorating.

We then create concrete decorator classes, such as **`Milk`** and **`WhippedCream`**, which inherit from the **`CoffeeDecorator`** abstract class. These decorators can be dynamically attached to any **`ICoffee`** object, allowing us to extend the object's functionality without modifying its existing behavior. This pattern makes it easy to add new features and combine them as needed.