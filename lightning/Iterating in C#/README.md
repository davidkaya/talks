# Iterating in C#

A lightning talk covering all the ways to iterate in C# — from basic `for` and `foreach` to `Span<T>`, `await foreach`, and parallel loops.

## Topics Covered

- `for`, `foreach`, `while` loops
- `ref foreach` for zero-copy iteration
- LINQ lazy evaluation and pitfalls
- `Parallel.ForEach` for CPU-bound work
- `await foreach` with `IAsyncEnumerable<T>`
- High-performance iteration with `Span<T>`
- Mutating collections safely during iteration
- Custom iterators with `yield return`

## Running the Demo

```bash
cd demo/IterationDemo
dotnet run
```
