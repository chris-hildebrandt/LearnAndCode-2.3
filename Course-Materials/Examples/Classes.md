# **Classes - Examples**

This README provides examples related to organizing classes with a focus on modularity and maintainability. These examples demonstrate how applying principles learned from Clean Code and other resources can result in improvements to your codebase.

## **Table of Contents**

- **[Example 1: Improving Class Organization](#example-1-improving-class-organization)**
- **[Example 2: Dependency Management](#example-3-dependency-management)**

## **Example 1: Improving Class Organization**

Before:

```cs
public class UserManager
{
    public void CreateUser(string name, string email)
    {
        // User creation logic
    }

    public User GetUserByEmail(string email)
    {
        // Retrieving user by email logic
    }

    public void SendEmail(string email, string subject, string body)
    {
        // Sending email logic
    }
}

```

After refactoring:

```cs
public class UserCreator
{
    public void CreateUser(string name, string email)
    {
        // User creation logic
    }
}

public class UserRepository
{
    public User GetUserByEmail(string email)
    {
        // Retrieving user by email logic
    }
}

public class EmailSender
{
    public void SendEmail(string email, string subject, string body)
    {
        // Sending email logic
    }
}

```

**Explanation:**

In the initial example, the **`UserManager`** class has multiple responsibilities: creating users, retrieving users, and sending emails. After refactoring, the responsibilities are separated into three classes: **`UserCreator`**, **`UserRepository`**, and **`EmailSender`**. This separation adheres to the Single Responsibility Principle and results in more maintainable code.

## **Example 2: Dependency Management**

Before:

```cs
public class OrderProcessor
{
    private OrderValidator _orderValidator = new OrderValidator();
    private OrderRepository _orderRepository = new OrderRepository();

    public void ProcessOrder(Order order)
    {
        if (_orderValidator.IsValid(order))
        {
            _orderRepository.Save(order);
        }
    }
}

```

After refactoring:

```cs
public class OrderProcessor
{
    private readonly IOrderValidator _orderValidator;
    private readonly IOrderRepository _orderRepository;

    public OrderProcessor(IOrderValidator orderValidator, IOrderRepository orderRepository)
    {
        _orderValidator = orderValidator;
        _orderRepository = orderRepository;
    }

    public void ProcessOrder(Order order)
    {
        if (_orderValidator.IsValid(order))
        {
            _orderRepository.Save(order);
        }
    }
}

```

**Explanation:**

In the initial example, the **`OrderProcessor`** class directly creates instances of **`OrderValidator`** and **`OrderRepository`**, creating tight coupling between the classes. This makes it difficult to test and maintain the code, as any changes to the validator or repository classes would require modifications to the **`OrderProcessor`** class.

To address this issue, the code is refactored to use dependency injection. Instead of directly creating instances of the **`OrderValidator`** and **`OrderRepository`** classes, the **`OrderProcessor`** class takes in instances of **`IOrderValidator`** and **`IOrderRepository`** interfaces through the constructor. This allows the dependencies to be injected into the class, making it easier to test and maintain.

By using interfaces, the **`OrderProcessor`** class is decoupled from specific implementations of the **`OrderValidator`** and **`OrderRepository`** classes, and can work with any class that implements the required interfaces. This makes the code more flexible and extensible, and better aligns with In Time Tec's focus on modular, maintainable code and the Quality Manifesto's emphasis on technical excellence.