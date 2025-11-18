# **Systems - Examples**

This README provides examples related to organizing classes and systems with a focus on modularity and maintainability. These examples demonstrate how applying principles learned from Clean Code and other resources can result in improvements to your codebase.

## **Table of Contents**

- **[Example 1: Separation of Concerns in System Design](#example-2-separation-of-concerns-in-system-design)**



## **Example 1: Separation of Concerns in System Design**

Before:

```lua
|---------------------|
|    Web Application  |
|---------------------|

```

After refactoring:

```lua
|------------------|    |--------------------|    |---------------------|
|  Web Application |<-->|  Business Services |<-->|  Data Access Layer  |
|------------------|    |--------------------|    |---------------------|

```

**Explanation:**

In the initial example, the web application handles all aspects of the system, including presentation, business logic, and data access. After refactoring, the system is divided into three separate layers: the web application (presentation layer), the business services (business logic layer), and the data access layer. This separation of concerns leads to better modularity, making it easier to maintain and evolve the system.
