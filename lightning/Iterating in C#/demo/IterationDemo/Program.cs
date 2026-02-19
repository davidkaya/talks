/// <summary>
/// Demonstrates different iteration techniques in C#.
/// Each section corresponds to a slide in the presentation.
/// </summary>

var numbers = new List<int> { 10, 20, 30, 40, 50 };

// ─────────────────────────────────────────────
// 1. for loop — full index control
// ─────────────────────────────────────────────
Console.WriteLine("=== for loop ===");
for (int i = 0; i < numbers.Count; i++)
{
    Console.WriteLine($"  [{i}] = {numbers[i]}");
}

// ─────────────────────────────────────────────
// 2. foreach — idiomatic default
// ─────────────────────────────────────────────
Console.WriteLine("\n=== foreach ===");
foreach (var n in numbers)
{
    Console.WriteLine($"  {n}");
}

// ─────────────────────────────────────────────
// 3. ref foreach with Span<T> — zero-copy mutation
// ─────────────────────────────────────────────
Console.WriteLine("\n=== ref foreach (Span<T>) ===");
Span<int> span = stackalloc int[] { 1, 2, 3, 4, 5 };
foreach (ref int x in span)
{
    x *= 10; // ✅ mutates in place, no copy
}
foreach (var x in span)
{
    Console.WriteLine($"  {x}");
}

// ─────────────────────────────────────────────
// 4. while + manual enumerator
// ─────────────────────────────────────────────
Console.WriteLine("\n=== while (manual enumerator) ===");
using var enumerator = numbers.GetEnumerator();
while (enumerator.MoveNext())
{
    Console.WriteLine($"  {enumerator.Current}");
}

// ─────────────────────────────────────────────
// 5. LINQ — lazy, composable pipelines
// ─────────────────────────────────────────────
Console.WriteLine("\n=== LINQ ===");
var filtered = numbers
    .Where(n => n > 15)
    .Select(n => n * 2);

// ⚠️ Work only happens here, when we enumerate
foreach (var x in filtered)
{
    Console.WriteLine($"  {x}");
}

// ─────────────────────────────────────────────
// 6. Parallel.ForEach — CPU-bound parallelism
// ─────────────────────────────────────────────
Console.WriteLine("\n=== Parallel.ForEach ===");
Parallel.ForEach(numbers, n =>
{
    // ⚠️ Order is not guaranteed
    Console.WriteLine($"  Thread {Environment.CurrentManagedThreadId}: {n}");
});

// ─────────────────────────────────────────────
// 7. await foreach — async streaming
// ─────────────────────────────────────────────
Console.WriteLine("\n=== await foreach (IAsyncEnumerable) ===");
await foreach (var value in GenerateAsync(5))
{
    Console.WriteLine($"  {value}");
}

// ─────────────────────────────────────────────
// 8. Mutating while iterating — the right way
// ─────────────────────────────────────────────
Console.WriteLine("\n=== Safe mutation (backward for loop) ===");
var mutable = new List<int> { 1, 2, 3, 4, 5, 6 };

// ❌ foreach + Remove would throw InvalidOperationException
// ✅ Backward for loop is safe
for (int i = mutable.Count - 1; i >= 0; i--)
{
    if (mutable[i] % 2 == 0)
        mutable.RemoveAt(i);
}
Console.WriteLine($"  Remaining: {string.Join(", ", mutable)}");

// ─────────────────────────────────────────────
// 9. Custom iterator with yield return
// ─────────────────────────────────────────────
Console.WriteLine("\n=== Custom iterator (yield return) ===");
foreach (var fib in Fibonacci().Take(10))
{
    Console.Write($"  {fib}");
}
Console.WriteLine();

// ─── Helper methods ──────────────────────────

/// <summary>
/// Demonstrates IAsyncEnumerable with yield return.
/// Simulates streaming data arriving over time.
/// </summary>
static async IAsyncEnumerable<int> GenerateAsync(int count)
{
    for (int i = 1; i <= count; i++)
    {
        await Task.Delay(100); // simulate async work
        yield return i * 100;
    }
}

/// <summary>
/// Demonstrates a lazy infinite sequence using yield return.
/// Generates the Fibonacci sequence on demand.
/// </summary>
static IEnumerable<long> Fibonacci()
{
    long a = 0, b = 1;
    while (true)
    {
        yield return a;
        (a, b) = (b, a + b);
    }
}
