// This file is used to declare attributes that apply to the entire assembly (the compiled project).
// In this case, it's being used to make the internal members of this project (TaskFlowAPI)
// visible to another project, specifically our test project (TaskFlowAPI.Tests).

// An "assembly" is the main output of a .NET project, usually a .dll or .exe file.

using System.Runtime.CompilerServices; // This namespace contains the InternalsVisibleToAttribute class.

// This is an assembly-level attribute.
// [assembly: ... ] means the attribute applies to the entire assembly.

// `InternalsVisibleTo` is a special attribute that breaks the standard rules of encapsulation for a very specific purpose: TESTING.

// In C#, `internal` members are only accessible within the same project (assembly).
// However, our unit tests are in a separate project (`TaskFlowAPI.Tests`).
// To allow our test project to access `internal` classes and methods in this `TaskFlowAPI` project
// (like the `MapToDto` and `MapToEntity` methods in `TaskService`), we use this attribute.

// It essentially says, "Hey compiler, it's okay to let the 'TaskFlowAPI.Tests' project see all of my internal parts."
// This is crucial for thorough testing without having to make everything `public`.
[assembly: InternalsVisibleTo("TaskFlowAPI.Tests")]