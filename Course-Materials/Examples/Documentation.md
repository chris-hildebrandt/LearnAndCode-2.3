# **Documentation & Communication - Examples**

This README showcases examples of great vs bad documentation for the same C# feature, demonstrating the importance of clear and concise documentation for both internal use and client-facing communication. This aligns with In Time Tec's emphasis on collaboration and the Quality Manifesto's focus on customer-centric design.

## **Table of Contents**

1. **[Great Documentation](#great-documentation)**
2. **[Bad Documentation](#bad-documentation)**

## **Great Documentation**

### **Feature: File Content Reverser**

This feature reads the content of a text file and reverses the order of its characters, then saves the result in a new file. This can be useful for testing text processing algorithms and encoding techniques.

### **Requirements**

- Supported file formats: TXT, CSV
- Maximum file size: 10 MB

### **Usage**

1. Call the **`ReverseFileContent`** method, passing the input file path and the desired output file path as arguments.
2. The method will read the content of the input file, reverse the order of its characters, and save the result in the output file.

```cs
FileReverser reverser = new FileReverser();
reverser.ReverseFileContent("input.txt", "output.txt");

```

### **Notes**

- If the output file already exists, its content will be overwritten.
- If the input file is not found or not supported, an exception will be thrown.

## **Bad Documentation**

### **Feature: File Content Reverser**

This feature reverses a file's content.

### **How to Use**

1. Call the method with file paths.
2. Done.

```cs
FileReverser reverser = new FileReverser();
reverser.ReverseFileContent("input.txt", "output.txt");

```

### **Notes**

- Be careful with file formats and sizes.