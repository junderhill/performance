﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BenchmarkDotNet.Attributes;
using MicroBenchmarks;

namespace System.Reflection
{
    [BenchmarkCategory(Categories.CoreCLR, Categories.Reflection)]
    [GenericTypeArguments(typeof(EmptyStruct))] // value type
    [GenericTypeArguments(typeof(EmptyClass))] // reference type
    public class Activator<T>
    {
        private readonly string _assemblyName = typeof(T).Assembly.FullName;
        private readonly string _typeName = typeof(T).FullName;

        [Benchmark]
        public T CreateInstanceGeneric() => System.Activator.CreateInstance<T>();

        [Benchmark]
        public object CreateInstanceType() => System.Activator.CreateInstance(typeof(T));
    }

    public class EmptyClass { }
    public struct EmptyStruct { }
}
