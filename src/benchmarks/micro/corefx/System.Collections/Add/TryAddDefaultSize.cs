﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks;

namespace System.Collections
{
    [BenchmarkCategory(Categories.CoreFX, Categories.Collections, Categories.GenericCollections)]
    [GenericTypeArguments(typeof(int))] // value type
    [GenericTypeArguments(typeof(string))] // reference type
    public class TryAddDefaultSize<T>
    {
        private T[] _uniqueValues;

        [Params(Utils.DefaultCollectionSize)]
        public int Count;

        [GlobalSetup]
        public void Setup() => _uniqueValues = ValuesGenerator.ArrayOfUniqueValues<T>(Count);

        [Benchmark]
        public ConcurrentDictionary<T, T> ConcurrentDictionary()
        {
            var collection = new ConcurrentDictionary<T, T>();
            var uniqueValues = _uniqueValues;
            for(int i = 0; i < uniqueValues.Length; i++)
                collection.TryAdd(uniqueValues[i], uniqueValues[i]);
            return collection;
        }
    }
}