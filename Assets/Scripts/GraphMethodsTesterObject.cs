using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphMethodsTesterBehaviour : MonoBehaviour
{
    [Header("Integer test data")]
    [SerializeField] private List<int> intsA = new List<int>() { 1, 2, 2, 3, 4 };
    [SerializeField] private List<int> intsB = new List<int>() { 3, 4, 4, 5 };

    [Header("String test data")]
    [SerializeField] private List<string> stringsA = new List<string>() { "a", "b", "B", "c" };
    [SerializeField] private List<string> stringsB = new List<string>() { "b", "C", "d" };

    private void Awake()
    {
        GraphMethodsTester.RunAllTests(intsA, intsB, stringsA, stringsB);
        Debug.Log("\nALL TESTS FINISHED");
    }
}


public static class GraphMethodsTester
{
    public static void RunAllTests(
        List<int> intsA,
        List<int> intsB,
        List<string> stringsA,
        List<string> stringsB)
    {
        Debug.Log("=== RUNNING GraphMethods TESTS ===\n");

        TestInts(intsA, intsB);
        TestStrings(stringsA, stringsB);
        TestCustomEnumerable();

        Debug.Log("\n=== TESTS COMPLETE ===");
    }

    static void TestInts(List<int> a, List<int> b)
    {
        Debug.Log("--- TEST: INTEGERS ---");

        Print("A", a);
        Print("B", b);

        Debug.Log($"All(A, >0): {GraphMethods.All(a, x => x > 0)}");
        Debug.Log($"Any(A, ==2): {GraphMethods.Any(a, x => x == 2)}");

        Debug.Log($"Contains(A, 3): {GraphMethods.Contains(a, 3)}");

        PrintEnumerable("Distinct(A)", GraphMethods.Distinct(a));

        Debug.Log($"ElementAt(A, 2): {GraphMethods.ElementAt(a, 2)}");

        PrintEnumerable("Except(A, B)", GraphMethods.Except(a, b));

        Debug.Log($"First(A, even): {GraphMethods.First(a, x => x % 2 == 0)}");
        Debug.Log($"Last(A, <4): {GraphMethods.Last(a, x => x < 4)}");

        PrintEnumerable("Intersect(A,B)", GraphMethods.Intersect(a, b));

        Debug.Log($"Count(A, ==2): {GraphMethods.Count(a, x => x == 2)}");

        Debug.Log($"SequenceEqual(A, A): {GraphMethods.SequenceEqual(a, a, EqualityComparer<int>.Default)}");
        Debug.Log($"SequenceEqual(A, B): {GraphMethods.SequenceEqual(a, b, EqualityComparer<int>.Default)}");

        try
        {
            Debug.Log($"Single(A, ==3): {GraphMethods.Single(a, x => x == 3)}");
        }
        catch (Exception e) { Debug.Log($"Single(A, ==3) threw: {e.Message}"); }

        try
        {
            Debug.Log($"Single(A, ==2): {GraphMethods.Single(a, x => x == 2)}");
        }
        catch (Exception e) { Debug.Log($"Single(A, ==2) threw: {e.Message}"); }

        PrintEnumerable("SkipWhile(A, <3)", GraphMethods.SkipWhile(a, x => x < 3));
        PrintEnumerable("Where(A, >=3)", GraphMethods.Where(a, x => x >= 3));

        PrintEnumerable("Union(A,B)", GraphMethods.Union(a, b));
        Debug.Log("");
    }

    static void TestStrings(List<string> a, List<string> b)
    {
        Debug.Log("--- TEST: STRINGS ---");

        Print("A", a);
        Print("B", b);

        Debug.Log($"All(A, non-empty): {GraphMethods.All(a, s => !string.IsNullOrEmpty(s))}");
        Debug.Log($"Any(A, equals 'B'): {GraphMethods.Any(a, s => s == "B")}");

        Debug.Log($"Contains(A, 'b'): {GraphMethods.Contains(a, "b")}");

        var ci = StringComparer.OrdinalIgnoreCase;
        Debug.Log($"Contains(A, 'b', OrdinalIgnoreCase): {GraphMethods.Contains(a, "b", ci)}");
        Debug.Log($"Contains(A, 'B', OrdinalIgnoreCase): {GraphMethods.Contains(a, "B", ci)}");

        PrintEnumerable("Distinct(A)", GraphMethods.Distinct(a));
        PrintEnumerable("Distinct(A, OrdinalIgnoreCase)", GraphMethods.Distinct(a, ci));

        PrintEnumerable("Except(A,B) default", GraphMethods.Except(a, b));
        PrintEnumerable("Except(A,B) ci", GraphMethods.Except(a, b, ci));

        PrintEnumerable("Intersect(A,B) default", GraphMethods.Intersect(a, b));
        PrintEnumerable("Intersect(A,B) ci", GraphMethods.Intersect(a, b, ci));

        PrintEnumerable("Union(A,B)", GraphMethods.Union(a, b));
        PrintEnumerable("Union(A,B) ci", GraphMethods.Union(a, b, ci));

        Debug.Log($"ElementAt(A, 1): {GraphMethods.ElementAt(a, 1)}");

        Debug.Log($"First(A, starts with 'b' case-insensitive): {GraphMethods.First(a, s => s.Equals("b", StringComparison.OrdinalIgnoreCase))}");
        Debug.Log($"Last(A, length==1): {GraphMethods.Last(a, s => s.Length == 1)}");

        Debug.Log($"Count(A, equals 'b' case-insensitive): {GraphMethods.Count(a, s => s.Equals("b", StringComparison.OrdinalIgnoreCase))}");

        Debug.Log($"SequenceEqual(A, A): {GraphMethods.SequenceEqual(a, a, EqualityComparer<string>.Default)}");

        try
        {
            Debug.Log($"Single(A, equals 'c'): {GraphMethods.Single(a, s => s == "c")}");
        }
        catch (Exception e) { Debug.Log($"Single(A,'c') threw: {e.Message}"); }

        try
        {
            Debug.Log($"Single(A, equals 'b' ci): {GraphMethods.Single(a, s => s.Equals("b", StringComparison.OrdinalIgnoreCase))}");
        }
        catch (Exception e) { Debug.Log($"Single(A,'b' ci) threw: {e.Message}"); }

        PrintEnumerable("SkipWhile(A, =='a')", GraphMethods.SkipWhile(a, s => s == "a"));
        PrintEnumerable("Where(A, !='B' ci)", GraphMethods.Where(a, s => !s.Equals("B", StringComparison.OrdinalIgnoreCase)));

        Debug.Log("");
    }

    static void TestCustomEnumerable()
    {
        Debug.Log("--- TEST: CUSTOM IENUMERABLE ---");

        var custom = new MyEnumerable<int>(new int[] { 10, 20, 20, 30 });
        var list = new List<int> { 20, 30, 40 };

        Print("Custom", custom);
        Print("List", list);

        PrintEnumerable("Distinct(Custom)", GraphMethods.Distinct(custom));

        PrintEnumerable("Intersect(Custom, List)", GraphMethods.Intersect(custom, list));

        PrintEnumerable("Except(Custom, List)", GraphMethods.Except(custom, list));

        PrintEnumerable("Where(Custom, >15)", GraphMethods.Where(custom, x => x > 15));

        var sameCustom = new MyEnumerable<int>(new int[] { 10, 20, 20, 30 });
        Debug.Log($"SequenceEqual(custom, sameCustom): {GraphMethods.SequenceEqual(custom, sameCustom, EqualityComparer<int>.Default)}");

        Debug.Log($"ElementAt(custom, 2): {GraphMethods.ElementAt(custom, 2)}");

        Debug.Log("");
    }

    // helper printing
    static void Print<T>(string name, IEnumerable<T> seq)
    {
        Debug.Log($"{name}: {Stringify(seq)}");
    }

    static void PrintEnumerable<T>(string title, IEnumerable<T> seq)
    {
        Debug.Log($"{title}: {Stringify(seq)}");
    }

    static string Stringify<T>(IEnumerable<T> seq)
    {
        var items = new List<string>();
        foreach (var s in seq) items.Add(s?.ToString() ?? "null");
        return "[" + string.Join(", ", items) + "]";
    }
}

public class MyEnumerable<T> : IEnumerable<T>
{
    private readonly T[] _data;

    public MyEnumerable(T[] data)
    {
        _data = data ?? Array.Empty<T>();
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _data.Length; i++)
            yield return _data[i];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
