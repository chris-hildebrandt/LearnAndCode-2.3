This README showcases an example of refactoring code with a focus on improving meaningful names. The purpose of this example is to demonstrate the positive impact of clear and descriptive naming conventions on code readability and maintainability.

## **Table of Contents**

- **[Before Refactoring](#before-refactoring)**
- **[After Refactoring](#after-refactoring)**
- **[Refactoring Explanation](#refactoring-explanation)**

## **Before Refactoring**

```cs
// Sample code with poor naming conventions
using System;

public class Calculator
{
    public static double Calc(double a, double b, string op)
    {
        if (op == "add") return a + b;
        else if (op == "sub") return a - b;
        else if (op == "mul") return a * b;
        else if (op == "div") return a / b;
        else return double.NaN;
    }

    public static void Main(string[] args)
    {
        double result = Calc(3, 5, "add");
        Console.WriteLine(result);
    }
}

```

## **After Refactoring**

```cs
// Refactored code with improved naming conventions
using System;

public class Calculator
{
    public static double PerformOperation(double firstNumber, double secondNumber, string operation)
    {
        if (operation == "add") return firstNumber + secondNumber;
        else if (operation == "subtract") return firstNumber - secondNumber;
        else if (operation == "multiply") return firstNumber * secondNumber;
        else if (operation == "divide") return firstNumber / secondNumber;
        else return double.NaN;
    }

    public static void Main(string[] args)
    {
        double result = PerformOperation(3, 5, "add");
        Console.WriteLine(result);
    }
}

```

## **Refactoring Explanation**

In this refactoring example, we made the following changes to improve the meaningful names in the code:

1. Renamed the function **`Calc()`** to **`PerformOperation()`**. The new name provides a clearer description of the function's purpose.
2. Renamed the parameters **`a`**, **`b`**, and **`op`** to **`firstNumber`**, **`secondNumber`**, and **`operation`**, respectively. This makes it easier to understand the purpose of each parameter without having to read the entire function.
3. Changed the operation strings from abbreviated forms (e.g., "sub") to full words (e.g., "subtract"). This enhances readability and reduces the chance of confusion.

These changes make the code more readable and maintainable by providing clearer context and reducing ambiguity.