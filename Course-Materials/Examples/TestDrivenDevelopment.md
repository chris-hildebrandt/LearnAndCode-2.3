# **Unit Tests & Test-Driven Development - Example**

This README showcases examples of unit tests and test-driven development (TDD) to demonstrate the importance of these practices in creating high-quality, maintainable software.

## **Table of Contents**

- **[Before Unit Tests & TDD](#before-unit-tests--tdd)**
- **[Writing Unit Tests](#writing-unit-tests)**
- **[Test-Driven Development](#test-driven-development)**
- **[After Unit Tests & TDD](#after-unit-tests--tdd)**
- **[Impact of Unit Tests & TDD](#impact-of-unit-tests--tdd)**

## **Before Unit Tests & TDD**

```cs
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        return a - b;
    }

    public int Multiply(int a, int b)
    {
        return a * b;
    }

    public int Divide(int a, int b)
    {
        return a / b;
    }
}

```

## **Writing Unit Tests**

```cs
[TestFixture]
public class CalculatorTests
{
    private Calculator calculator;

    [SetUp]
    public void SetUp()
    {
        calculator = new Calculator();
    }

    [Test]
    public void Add_WhenCalled_ReturnsSum()
    {
        int result = calculator.Add(1, 2);

        Assert.AreEqual(3, result);
    }

    [Test]
    public void Subtract_WhenCalled_ReturnsDifference()
    {
        int result = calculator.Subtract(5, 2);

        Assert.AreEqual(3, result);
    }

    [Test]
    public void Multiply_WhenCalled_ReturnsProduct()
    {
        int result = calculator.Multiply(3, 4);

        Assert.AreEqual(12, result);
    }

    [Test]
    public void Divide_WhenCalled_ReturnsQuotient()
    {
        int result = calculator.Divide(10, 2);

        Assert.AreEqual(5, result);
    }
}
```

## **Test-Driven Development**

1. Write a failing test for the Square method:

```cs
[Test]
public void Square_WhenCalled_ReturnsSquare()
{
    int result = calculator.Square(4);

    Assert.AreEqual(16, result);
}
```

1. Implement the Square method in the Calculator class to make the test pass:

```cs
public int Square(int a)
{
    return a * a;
}
```

## **After Unit Tests & TDD**

```cs
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        return a - b;
    }

    public int Multiply(int a, int b)
    {
        return a * b;
    }

    public int Divide(int a, int b)
    {
        return a / b;
    }

    public int Square(int a)
    {
        return a * a;
    }
}
```

## **Impact of Unit Tests & TDD**

The introduction of unit tests and the use of TDD have led to the following improvements in the code:

- The unit tests ensure that the Calculator class's methods are working correctly, increasing confidence in the code's functionality.
- The test suite makes it easier to identify and fix bugs, as any changes that introduce issues will cause the tests to fail.
- Unit tests serve as documentation, providing examples of how to use the Calculator class's methods.
- The test-driven development process ensures that the new Square method is implemented correctly from the start, as it was guided by a test case.
- The presence of unit tests encourages a modular design that facilitates easier testing and maintenance of the code.

These changes have made the code more reliable, maintainable, and robust, showcasing the importance of unit tests and test-driven development in ensuring high-quality software.