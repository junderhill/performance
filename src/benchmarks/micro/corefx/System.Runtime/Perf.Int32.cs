// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BenchmarkDotNet.Attributes;
using MicroBenchmarks;

namespace System.Tests
{
    [BenchmarkCategory(Categories.CoreFX)]
    public class Perf_Int32
    {
        private char[] _destination = new char[int.MinValue.ToString().Length];
        
        public static IEnumerable<object> Values => new object[]
        {
            int.MinValue,
            4, // single digit
            (int)12345, // same value used by other tests to compare the perf
            int.MaxValue
        };

        public static IEnumerable<object> StringValuesDecimal => Values.Select(value => value.ToString()).ToArray();
        public static IEnumerable<object> StringValuesHex => Values.Select(value => ((int)value).ToString("X")).ToArray();

        [Benchmark]
        [ArgumentsSource(nameof(Values))]
        public string ToString(int value) => value.ToString();

        [Benchmark]
        [ArgumentsSource(nameof(Values))]
        public string ToStringHex(int value) => value.ToString("X");

        [Benchmark]
        [ArgumentsSource(nameof(StringValuesDecimal))]
        public int Parse(string value) => int.Parse(value);

        [Benchmark]
        [ArgumentsSource(nameof(StringValuesHex))]
        public int ParseHex(string value) => int.Parse(value, NumberStyles.HexNumber);

        [Benchmark]
        [ArgumentsSource(nameof(StringValuesDecimal))]
        public bool TryParse(string value) => int.TryParse(value, out _);
    }
}
