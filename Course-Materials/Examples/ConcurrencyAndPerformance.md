# **Concurrency & Performance - Examples**

This README provides examples related to concurrent programming and performance optimization, demonstrating how applying the principles learned from Clean Code and other resources can result in improvements to your codebase.

## **Table of Contents**

- **[Example 1: Using Parallelism for Performance Improvement](#example-1-using-parallelism-for-performance-improvement)**
- **[Example 2: Implementing a Thread-Safe Data Structure](#example-2-implementing-a-thread-safe-data-structure)**
- **[Example 3: Caching for Performance Optimization](#example-3-caching-for-performance-optimization)**

## **Example 1: Using Parallelism for Performance Improvement**

Before:

```cs
public void ProcessData(List<Data> dataList)
{
    foreach (var data in dataList)
    {
        ProcessSingleData(data);
    }
}

private void ProcessSingleData(Data data)
{
    // Processing logic
}

```

After refactoring:

```cs
public void ProcessData(List<Data> dataList)
{
    Parallel.ForEach(dataList, data =>
    {
        ProcessSingleData(data);
    });
}

private void ProcessSingleData(Data data)
{
    // Processing logic
}

```

**Explanation:**

In the initial example, the **`ProcessData`** method processes a list of data items sequentially using a **`foreach`** loop. After refactoring, the **`Parallel.ForEach`** method is used to process the data items concurrently, which can lead to performance improvements on multi-core processors.

## **Example 2: Implementing a Thread-Safe Data Structure**

Before:

```cs
public class Counter
{
    private int _count;

    public void Increment()
    {
        _count++;
    }

    public int GetCount()
    {
        return _count;
    }
}

```

After refactoring:

```cs
public class Counter
{
    private int _count;

    private readonly object _lockObject = new object();

    public void Increment()
    {
        lock (_lockObject)
        {
            _count++;
        }
    }

    public int GetCount()
    {
        lock (_lockObject)
        {
            return _count;
        }
    }
}

```

**Explanation:**

In the initial example, the **`Counter`** class is not thread-safe, which can lead to incorrect results when used in a multi-threaded environment. After refactoring, a **`lock`** statement is used to synchronize access to the **`_count`** variable, ensuring that the **`Increment`** and **`GetCount`** methods are thread-safe.

## **Example 3: Caching for Performance Optimization**

Before:

```cs
public class DataService
{
    public Data GetData(int id)
    {
        // Expensive data retrieval logic
    }
}

```

After refactoring:

```cs
public class DataService
{
    private readonly Dictionary<int, Data> _cache = new Dictionary<int, Data>();

    public Data GetData(int id)
    {
        if (!_cache.TryGetValue(id, out Data data))
        {
            // Expensive data retrieval logic
            _cache[id] = data;
        }

        return data;
    }
}

```

**Explanation:**

In the initial example, the **`DataService`** class retrieves data using an expensive operation every time the **`GetData`** method is called. After refactoring, a cache is implemented using a **`Dictionary`**, which stores previously retrieved data items. When the **`GetData`** method is called, the cache is checked first to see if the data item already exists. If it does, the cached data is returned, avoiding the expensive operation and improving performance.