# **Code Smells & Heuristics - Examples**

This README showcases examples of common code smells and how to refactor them, demonstrating the importance of addressing these issues to maintain code quality and adhere to In Time Tec's commitment to continuous improvement and technical excellence.

## **Table of Contents**

1. **[Long Method](#long-method)**
2. **[Large Class](#large-class)**
3. **[Duplicated Code](#duplicated-code)**

## **Long Method**

### **Code Smell**

A method is too long, making it difficult to understand, maintain, and modify.

```cs
public void ProcessOrder(Order order)
{
    // Validate order
    if (order == null || order.Items.Count == 0)
    {
        throw new ArgumentException("Invalid order");
    }

    // Calculate total price
    decimal totalPrice = 0;
    foreach (var item in order.Items)
    {
        totalPrice += item.Price * item.Quantity;
    }

    // Apply discount
    if (order.Customer.IsPreferred)
    {
        totalPrice *= 0.9m;
    }

    // Create invoice
    var invoice = new Invoice(order.Customer, totalPrice);

    // Save invoice to database
    _invoiceRepository.Save(invoice);

    // Send invoice by email
    _emailService.SendInvoice(invoice);
}

```

### **Refactored Code**

Break the long method into smaller, more focused methods.

```cs
public void ProcessOrder(Order order)
{
    ValidateOrder(order);
    decimal totalPrice = CalculateTotalPrice(order);
    totalPrice = ApplyDiscount(order, totalPrice);
    var invoice = CreateInvoice(order, totalPrice);
    SaveInvoice(invoice);
    SendInvoiceByEmail(invoice);
}

private void ValidateOrder(Order order)
{
    if (order == null || order.Items.Count == 0)
    {
        throw new ArgumentException("Invalid order");
    }
}

private decimal CalculateTotalPrice(Order order)
{
    decimal totalPrice = 0;
    foreach (var item in order.Items)
    {
        totalPrice += item.Price * item.Quantity;
    }
    return totalPrice;
}

private decimal ApplyDiscount(Order order, decimal totalPrice)
{
    if (order.Customer.IsPreferred)
    {
        totalPrice *= 0.9m;
    }
    return totalPrice;
}

private Invoice CreateInvoice(Order order, decimal totalPrice)
{
    return new Invoice(order.Customer, totalPrice);
}

private void SaveInvoice(Invoice invoice)
{
    _invoiceRepository.Save(invoice);
}

private void SendInvoiceByEmail(Invoice invoice)
{
    _emailService.SendInvoice(invoice);
}

```

## **Large Class**

### **Code Smell**

A class tries to do too much, making it difficult to understand, maintain, and modify.

```cs
public class OrderProcessor
{
    public void ProcessOrder(Order order) { /* ... */ }
    public decimal CalculateTotalPrice(Order order) { /* ... */ }
    public decimal ApplyDiscount(Order order, decimal totalPrice) { /* ... */ }
    public Invoice CreateInvoice(Order order, decimal totalPrice) { /* ... */ }
    public void SaveInvoice(Invoice invoice) { /* ... */ }
    public void SendInvoiceByEmail(Invoice invoice) { /* ... */ }
    public void ValidateCustomer(Customer customer) { /* ... */ }
    public void NotifyCustomer(Customer customer) { /* ... */ }
    public void UpdateCustomerStatus(Customer customer) { /* ... */ }
}

```

### **Refactored Code**

Split the large class into smaller, more focused classes.

```cs
public class OrderProcessor
{
    private readonly InvoiceService _invoiceService;
    private readonly CustomerService _customerService;

    public OrderProcessor(InvoiceService invoiceService, CustomerService customerService)
    {
        _invoiceService = invoiceService;
        _customerService = customerService;
    }

    public void ProcessOrder(Order order)
    {
        _invoiceService.ProcessInvoice(order);
        _customerService.HandleCustomer(order.Customer);
    }
}

public class InvoiceService
{
    public void ProcessInvoice(Order order) { /* ... */ }
    public decimal CalculateTotalPrice(Order order) { /* ... */ }
    public decimal ApplyDiscount(Order order, decimal totalPrice) { /* ... */ }
    public Invoice CreateInvoice(Order order, decimal totalPrice) { /* ... */ }
    public void SaveInvoice(Invoice invoice) { /* ... */ }
    public void SendInvoiceByEmail(Invoice invoice) { /* ... */ }
}

public class CustomerService
{
    public void ValidateCustomer(Customer customer) { /* ... */ }
    public void NotifyCustomer(Customer customer) { /* ... */ }
    public void UpdateCustomerStatus(Customer customer) { /* ... */ }
}

```

## **Duplicated Code**

### **Code Smell**

Code is duplicated in multiple places, making it difficult to maintain and modify.

```cs
public decimal CalculatePriceForProductA(int quantity)
{
    decimal basePrice = 100;
    decimal discount = 0.1m;
    return basePrice * quantity * (1 - discount);
}

public decimal CalculatePriceForProductB(int quantity)
{
    decimal basePrice = 200;
    decimal discount = 0.1m;
    return basePrice * quantity * (1 - discount);
}

```

### **Refactored Code**

Extract the duplicated code into a reusable method.

```cs
public decimal CalculatePrice(decimal basePrice, int quantity, decimal discount)
{
    return basePrice * quantity * (1 - discount);
}
```