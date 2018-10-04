using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEditor;
using Debug = UnityEngine.Debug;

public static class SearchProviderStopwatch
{
    private const int NumSamples = 200;
    private const int NumArrayItems = 50000000;

    [MenuItem("Kata/Measure Search Times")]
    public static void MeasureSearchTimes()
    {
        var binaryIterativeSamples = new List<long>();
        var binaryRecursiveSamples = new List<long>();
        var linqSearchSamples = new List<long>();

        for (var sample = 0; sample < NumSamples; sample++)
        {
            var array = GetRandomArray();

            var swBinaryIterative = new Stopwatch();
            var binaryIterative = new BinarySearchIterative();
            swBinaryIterative.Start();
            binaryIterative.FindKey(12, array);
            swBinaryIterative.Stop();

            var swBinaryRecursive = new Stopwatch();
            var binaryRecursive = new BinarySearchRecursive();
            swBinaryRecursive.Start();
            binaryRecursive.FindKey(12, array);
            swBinaryRecursive.Stop();

            var swLinqSearch = new Stopwatch();
            var linqSearch = new LinqIndexOfSearch();
            swLinqSearch.Start();
            linqSearch.FindKey(12, array);
            swLinqSearch.Stop();

            binaryIterativeSamples.Add(swBinaryIterative.ElapsedMilliseconds);
            binaryRecursiveSamples.Add(swBinaryRecursive.ElapsedMilliseconds);
            linqSearchSamples.Add(swLinqSearch.ElapsedMilliseconds);

            EditorUtility.DisplayProgressBar("Generating Samples",
                string.Format("Sample {0} of {1}", sample, NumSamples), sample / (float) NumSamples);
        }

        EditorUtility.ClearProgressBar();

        var iterativeAverage = binaryIterativeSamples.Sum(x => x) / NumSamples;
        var iterativeStd =
            Math.Sqrt(binaryIterativeSamples.Sum(x => Math.Pow(x - iterativeAverage, 2)) / (NumSamples - 1));
        var recursiveAverage = binaryRecursiveSamples.Sum(x => x) / NumSamples;
        var recursiveStd =
            Math.Sqrt(binaryRecursiveSamples.Sum(x => Math.Pow(x - recursiveAverage, 2)) / (NumSamples - 1));
        var linqAverage = linqSearchSamples.Sum(x => x) / NumSamples;
        var linqStd = 
            Math.Sqrt(linqSearchSamples.Sum(x => Math.Pow(x - linqAverage, 2)) / (NumSamples - 1));
        
        Debug.LogFormat("Binary Iterative: average {0} ms, standard deviation {1:F3} ms", iterativeAverage, iterativeStd);
        Debug.LogFormat("Binary Recursive: average {0} ms, standard deviation {1:F3} ms", recursiveAverage, recursiveStd);
        Debug.LogFormat("Linq IndexOf: average {0} ms, standard deviation {1:F3} ms", linqAverage, linqStd);
    }

    private static int[] GetRandomArray()
    {
        var random = new Random();
        var array = new int[NumArrayItems];
        for (var item = 0; item < NumArrayItems; item++)
        {
            var min = item > 0 ? array[item - 1] : 0;
            array[item] = random.Next(min, NumArrayItems);
        }

        return array;
    }
}