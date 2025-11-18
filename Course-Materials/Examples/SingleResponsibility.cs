// Example of what not to do when it comes to single responsibility rule

using System;

public class Employee
{
    // This class violates the single responsibility rule
    // It has two responsibilities: saving to database and generating report
    // It should be split into three classes: Employee (data), EmployeeRepository (saving), EmployeeReport (reporting)
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }

    public void Save()
    {
        // Save employee to database
    }

    public void GenerateReport()
    {
        // Generate report
    }
}

public class Program
{
    public static void Main()
    {
        var employee = new Employee();
        employee.Save();
        employee.GenerateReport();
        employee.Salary = 1000;
    }
}


// Good example of single responsibility

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
}

public class EmployeeRepository
{
    public void Save(Employee employee)
    {
        // Save employee to database
    }
}

public class EmployeeReport
{
    public void GenerateReport(Employee employee)
    {
        // Generate report
    }
}

public class Program
{
    public static void Main()
    {
        var employee = new Employee();
        var employeeRepository = new EmployeeRepository();
        var employeeReport = new EmployeeReport();
        employeeRepository.Save(employee);
        employeeReport.GenerateReport(employee);
    }
}


// Best example of single responsibility

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
}

public class EmployeeRepository
{
    public void Save(Employee employee)
    {
        // Save employee to database
    }
}

public class EmployeeReport
{
    public void GenerateReport(Employee employee)
    {
        // Generate report
    }
}

public class EmployeeService
{
    private readonly EmployeeRepository _employeeRepository;

    public EmployeeService(EmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    public void Save(Employee employee)
    {
        var employeeRepository = new EmployeeRepository();
        employee.Department = "IT";
        employeeRepository.Save(employee);
    }
}

public class Program
{
    public static void Main()
    {
        var employee = new Employee();
        var employeeRepository = new EmployeeRepository();
        var employeeReport = new EmployeeReport();
        var employeeService = new EmployeeService(employeeRepository);
        employeeService.Save(employee);
        employeeReport.GenerateReport(employee);
    }
}