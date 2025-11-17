using System;
using System.Collections.Generic;

public class GraphMethods
{
    /// <summary>
    /// Determines whether all elements of a sequence satisfy a condition.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool All<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        foreach (var item in source)
            if (!predicate(item))
                return false;

        return true;
    }


    /// <summary>
    /// Determines whether any element of a sequence satisfies a condition.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool Any<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate) 
    {
        foreach (var item in source)
            if (predicate(item))
                return true;

        return false;
    }


    /// <summary>
    /// Determines whether a sequence contains a specified element by using the default equality comparer.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static bool Contains<TSource>(IEnumerable<TSource> source, TSource item)
    {
        var comparer = EqualityComparer<TSource>.Default;

        foreach (var element in source)
            if (comparer.Equals(element, item))
                return true;

        return false;
    }


    /// <summary>
    /// Determines whether a sequence contains a specified element by using a specified IEqualityComparer<T>.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="item"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static bool Contains<TSource>(IEnumerable<TSource> source, TSource item, IEqualityComparer<TSource> comparer)
    {
        foreach (var element in source)
            if (comparer.Equals(element, item))
                return true;

        return false;
    }


    /// <summary>
    /// Returns distinct elements from a sequence by using the default equality comparer to compare values.
    /// Time: O(n)
    /// Memory: O(n)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Distinct<TSource>(IEnumerable<TSource> source)
    {
        var set = new HashSet<TSource>();
        var result = new List<TSource>();

        foreach (var item in source)
            if (set.Add(item))
                result.Add(item);

        return result;
    }


    /// <summary>
    /// Returns distinct elements from a sequence by using a specified IEqualityComparer<T> to compare values.
    /// Time: O(n)
    /// Memory: O(n)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Distinct<TSource>(IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
    {
        var set = new HashSet<TSource>(comparer);
        var result = new List<TSource>();

        foreach (var item in source)
            if (set.Add(item))
                result.Add(item);

        return result;
    }


    /// <summary>
    /// Returns the element at a specified index in a sequence.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static TSource ElementAt<TSource>(IEnumerable<TSource> source, int index)
    {
        int i = 0;

        foreach (var item in source)
        {
            if (i == index)
                return item;

            i++;
        }

        throw new ArgumentOutOfRangeException(nameof(index));
    }


    /// <summary>
    /// Produces the set difference of two sequences by using the default equality comparer to compare values.
    /// Time: O(n + m)
    /// Memory: O(n)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source1"></param>
    /// <param name="source2"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Except<TSource>(IEnumerable<TSource> source1, IEnumerable<TSource> source2)
    {
        var excluded = new HashSet<TSource>(source2);
        var yielded = new HashSet<TSource>();
        var result = new List<TSource>();

        foreach (var item in source1)
        {
            if (!excluded.Contains(item) && yielded.Add(item))
                result.Add(item);
        }

        return result;
    }


    /// <summary>
    /// Produces the set difference of two sequences by using the specified IEqualityComparer<T> to compare values.
    /// Time: O(n + m)
    /// Memory: O(n)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source1"></param>
    /// <param name="source2"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Except<TSource>(IEnumerable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
    {
        var excluded = new HashSet<TSource>(source2, comparer);
        var result = new List<TSource>();

        foreach (var item in source1)
            if (!excluded.Contains(item))
                result.Add(item);

        return result;
    }


    /// <summary>
    /// Returns the first element in a sequence that satisfies a specified condition.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static TSource First<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        foreach (var item in source)
            if (predicate(item))
                return item;

        throw new InvalidOperationException("No element matches predicate");
    }


    /// <summary>
    /// Returns the last element of a sequence that satisfies a specified condition.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static TSource Last<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        bool found = false;
        TSource lastMatch = default;

        foreach (var item in source)
        {
            if (predicate(item))
            {
                found = true;
                lastMatch = item;
            }
        }

        if (found)
            return lastMatch;

        throw new InvalidOperationException("No element matches predicate");
    }


    /// <summary>
    /// Produces the set intersection of two sequences by using the default equality comparer to compare values.
    /// Time: O(n + m)
    /// Memory: O(n)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source1"></param>
    /// <param name="source2"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Intersect<TSource>(IEnumerable<TSource> source1, IEnumerable<TSource> source2)
    {
        var set = new HashSet<TSource>(source2, EqualityComparer<TSource>.Default);
        var yielded = new HashSet<TSource>();
        var result = new List<TSource>();

        foreach (var item in source1)
            if (set.Contains(item) && yielded.Add(item))
                result.Add(item);

        return result;
    }


    /// <summary>
    /// Produces the set intersection of two sequences by using the specified IEqualityComparer<T> to compare values.
    /// Time: O(n + m)
    /// Memory: O(n)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source1"></param>
    /// <param name="source2"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Intersect<TSource>(IEnumerable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
    {
        var set = new HashSet<TSource>(source2, comparer);
        var yielded = new HashSet<TSource>(comparer);
        var result = new List<TSource>();

        foreach (var item in source1)
            if (set.Contains(item) && yielded.Add(item))
                result.Add(item);

        return result;
    }


    /// <summary>
    /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static int Count<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        int count = 0;

        foreach (var item in source)
            if (predicate(item))
                count++;

        return count;
    }


    /// <summary>
    /// Determines whether two sequences are equal by comparing their elements by using a specified IEqualityComparer<T>.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source1"></param>
    /// <param name="source2"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static bool SequenceEqual<TSource>(IEnumerable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
    {
        var e1 = source1.GetEnumerator();
        var e2 = source2.GetEnumerator();

        while (e1.MoveNext())
        {
            if (!e2.MoveNext())
                return false;

            if (!comparer.Equals(e1.Current, e2.Current))
                return false;
        }

        return !e2.MoveNext();
    }


    /// <summary>
    /// Returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static TSource Single<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        bool found = false;
        TSource match = default;

        foreach (var item in source)
        {
            if (predicate(item))
            {
                if (found)
                    throw new InvalidOperationException("More than one element matches.");

                found = true;
                match = item;
            }
        }

        if (!found)
            throw new InvalidOperationException("No element matches.");

        return match;
    }


    /// <summary>
    /// Bypasses elements in a sequence as long as a specified condition is true and then returns the remaining elements.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> SkipWhile<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var result = new List<TSource>();
        bool skipping = true;

        foreach (var item in source)
        {
            if (skipping && !predicate(item))
                skipping = false;

            if (!skipping)
                result.Add(item);
        }

        return result;
    }


    /// <summary>
    /// Produces the set union of two sequences by using the default equality comparer.
    /// Time: O(n + m)
    /// Memory: O(n + m)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source1"></param>
    /// <param name="source2"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Union<TSource>(IEnumerable<TSource> source1, IEnumerable<TSource> source2)
    {
        var seen = new HashSet<TSource>(EqualityComparer<TSource>.Default);
        var result = new List<TSource>();

        foreach (var item in source1)
            if (seen.Add(item))
                result.Add(item);

        foreach (var item in source2)
            if (seen.Add(item))
                result.Add(item);

        return result;
    }


    /// <summary>
    /// Produces the set union of two sequences by using a specified IEqualityComparer<T>.
    /// Time: O(n + m)
    /// Memory: O(n + m)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source1"></param>
    /// <param name="source2"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Union<TSource>(IEnumerable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
    {
        var seen = new HashSet<TSource>(comparer);
        var result = new List<TSource>();

        foreach (var item in source1)
            if (seen.Add(item))
                result.Add(item);

        foreach (var item in source2)
            if (seen.Add(item))
                result.Add(item);

        return result;
    }


    /// <summary>
    /// Filters a sequence of values based on a predicate. Each element's index is used in the logic of the predicate function.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Where<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var result = new List<TSource>();

        foreach (var item in source)
            if (predicate(item))
                result.Add(item);

        return result;
    }
}
