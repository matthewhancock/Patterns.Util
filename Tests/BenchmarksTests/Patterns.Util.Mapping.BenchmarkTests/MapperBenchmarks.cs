using System;
using BenchmarkDotNet.Attributes;
using Patterns.Util.Mapping.Mappers;

namespace Patterns.Util.Mapping.BenchmarkTests {

    [MemoryDiagnoser]
    public class MapperBenchmarks {
        private EntityA entity;
        private ModelA model;

        [GlobalSetup]
        public void Setup() {
            entity = new EntityA() { Name = nameof(EntityA) };
            model = new ModelA();
        }

        /*
|               Method |       Mean |    Error |   StdDev |     Median | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |-----------:|---------:|---------:|-----------:|------:|------:|------:|----------:|
|             MapValue | 1,565.6 ns | 223.8 ns | 645.8 ns | 1,500.0 ns |     - |     - |     - |     208 B |
| MapValueWithoutLocal |   352.1 ns | 100.5 ns | 291.6 ns |   250.0 ns |     - |     - |     - |     144 B |

|                                  Method |       Mean |     Error |   StdDev |        Median | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------- |-----------:|----------:|---------:|--------------:|------:|------:|------:|----------:|
|                                MapValue | 2,423.2 ns | 285.62 ns | 819.5 ns | 2,200.0000 ns |     - |     - |     - |     208 B |
|                    MapValueWithoutLocal |   102.1 ns |  48.22 ns | 138.4 ns |     0.0000 ns |     - |     - |     - |     144 B |
| MapValueWithoutLocalAndConstantDelegate | 1,366.7 ns | 234.34 ns | 653.3 ns | 1,300.0000 ns |     - |     - |     - |     208 B |

|                                  Method |       Mean |     Error |   StdDev |     Median | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------- |-----------:|----------:|---------:|-----------:|------:|------:|------:|----------:|
|                                MapValue | 2,000.0 ns | 307.74 ns | 892.8 ns | 1,800.0 ns |     - |     - |     - |     208 B |
|                    MapValueWithoutLocal |   548.4 ns |  99.65 ns | 279.4 ns |   500.0 ns |     - |     - |     - |     144 B |
| MapValueWithoutLocalAndConstantDelegate | 1,875.3 ns | 251.40 ns | 713.2 ns | 1,800.0 ns |     - |     - |     - |     208 B |
|             MapValueWithoutGenericValue |   388.9 ns | 100.96 ns | 281.4 ns |   300.0 ns |     - |     - |     - |     144 B |
|                 MapValueWithoutGenerics |   511.2 ns |  81.69 ns | 226.4 ns |   500.0 ns |     - |     - |     - |     144 B |

|                                  Method |       Mean |     Error |     StdDev |     Median | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------- |-----------:|----------:|-----------:|-----------:|------:|------:|------:|----------:|
|                                MapValue | 2,261.1 ns | 296.54 ns |   850.8 ns | 2,000.0 ns |     - |     - |     - |     208 B |
|                    MapValueWithoutLocal |   401.1 ns | 106.23 ns |   299.6 ns |   300.0 ns |     - |     - |     - |     144 B |
| MapValueWithoutLocalAndConstantDelegate | 2,430.5 ns | 388.04 ns | 1,113.4 ns | 2,100.0 ns |     - |     - |     - |     208 B |
|             MapValueWithoutGenericValue |   514.7 ns |  98.04 ns |   276.5 ns |   450.0 ns |     - |     - |     - |     144 B |
|                 MapValueWithoutGenerics |   323.2 ns | 104.55 ns |   300.0 ns |   250.0 ns |     - |     - |     - |     144 B |
|                 MapValueWithoutDelegate |   345.8 ns |  83.33 ns |   239.1 ns |   250.0 ns |     - |     - |     - |     144 B |

|                                  Method |        Mean |     Error |      StdDev |        Median | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------- |------------:|----------:|------------:|--------------:|------:|------:|------:|----------:|
|                                MapValue | 1,863.54 ns | 237.91 ns |   686.43 ns | 1,800.0000 ns |     - |     - |     - |     208 B |
|                    MapValueWithoutLocal |   584.69 ns |  95.62 ns |   278.93 ns |   550.0000 ns |     - |     - |     - |     144 B |
| MapValueWithoutLocalAndConstantDelegate | 2,541.67 ns | 392.21 ns | 1,131.62 ns | 2,450.0000 ns |     - |     - |     - |     208 B |
|             MapValueWithoutGenericValue |   229.79 ns |  79.27 ns |   226.16 ns |   200.0000 ns |     - |     - |     - |     144 B |
|                 MapValueWithoutGenerics |   226.80 ns |  78.74 ns |   228.45 ns |   200.0000 ns |     - |     - |     - |     144 B |
|                 MapValueWithoutDelegate |   188.54 ns |  65.16 ns |   187.99 ns |   100.0000 ns |     - |     - |     - |     144 B |
|                         MapValueWithRef |    14.74 ns |  19.00 ns |    54.52 ns |     0.0000 ns |     - |     - |     - |     144 B |

|                                  Method |        Mean |     Error |    StdDev |        Median | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------- |------------:|----------:|----------:|--------------:|------:|------:|------:|----------:|
|                                MapValue | 2,439.13 ns | 223.18 ns | 629.49 ns | 2,400.0000 ns |     - |     - |     - |     208 B |
|                    MapValueWithoutLocal |   221.65 ns |  42.57 ns | 123.51 ns |   200.0000 ns |     - |     - |     - |     144 B |
| MapValueWithoutLocalAndConstantDelegate | 1,201.61 ns | 144.47 ns | 409.83 ns | 1,150.0000 ns |     - |     - |     - |     208 B |
|             MapValueWithoutGenericValue |   282.11 ns | 116.04 ns | 332.94 ns |   200.0000 ns |     - |     - |     - |     144 B |
|                 MapValueWithoutGenerics |   281.91 ns | 115.85 ns | 330.51 ns |   150.0000 ns |     - |     - |     - |     144 B |
|                 MapValueWithoutDelegate |   276.04 ns |  82.90 ns | 239.18 ns |   200.0000 ns |     - |     - |     - |     144 B |
|                         MapValueWithRef |    82.45 ns |  49.21 ns | 140.41 ns |     0.0000 ns |     - |     - |     - |     144 B |
|        WhereAreTheAllocationsComingFrom |   153.06 ns |  85.15 ns | 248.38 ns |     0.0000 ns |     - |     - |     - |     144 B |

|                                  Method |        Mean |     Error |    StdDev |        Median | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------- |------------:|----------:|----------:|--------------:|------:|------:|------:|----------:|
|                                MapValue | 2,439.13 ns | 223.18 ns | 629.49 ns | 2,400.0000 ns |     - |     - |     - |     208 B |
|                    MapValueWithoutLocal |   221.65 ns |  42.57 ns | 123.51 ns |   200.0000 ns |     - |     - |     - |     144 B |
| MapValueWithoutLocalAndConstantDelegate | 1,201.61 ns | 144.47 ns | 409.83 ns | 1,150.0000 ns |     - |     - |     - |     208 B |
|             MapValueWithoutGenericValue |   282.11 ns | 116.04 ns | 332.94 ns |   200.0000 ns |     - |     - |     - |     144 B |
|                 MapValueWithoutGenerics |   281.91 ns | 115.85 ns | 330.51 ns |   150.0000 ns |     - |     - |     - |     144 B |
|                 MapValueWithoutDelegate |   276.04 ns |  82.90 ns | 239.18 ns |   200.0000 ns |     - |     - |     - |     144 B |
|                         MapValueWithRef |    82.45 ns |  49.21 ns | 140.41 ns |     0.0000 ns |     - |     - |     - |     144 B |
|        WhereAreTheAllocationsComingFrom |   153.06 ns |  85.15 ns | 248.38 ns |     0.0000 ns |     - |     - |     - |     144 B |
        
// switch from iteration setup to global setup
|                                  Method |       Mean |     Error |    StdDev |     Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------- |-----------:|----------:|----------:|-----------:|-------:|------:|------:|----------:|
|                                MapValue | 15.6289 ns | 0.7617 ns | 2.2458 ns | 15.1163 ns | 0.0153 |     - |     - |      64 B |
|                    MapValueWithoutLocal |  7.2409 ns | 0.3968 ns | 1.1701 ns |  7.1386 ns |      - |     - |     - |         - |
| MapValueWithoutLocalAndConstantDelegate | 16.7989 ns | 0.8563 ns | 2.5248 ns | 16.5048 ns | 0.0153 |     - |     - |      64 B |
|             MapValueWithoutGenericValue |  6.9387 ns | 0.6134 ns | 1.8085 ns |  6.6096 ns |      - |     - |     - |         - |
|                 MapValueWithoutGenerics |  4.4256 ns | 0.1220 ns | 0.1199 ns |  4.3660 ns |      - |     - |     - |         - |
|                 MapValueWithoutDelegate |  4.6249 ns | 0.1229 ns | 0.1509 ns |  4.6101 ns |      - |     - |     - |         - |
|                         MapValueWithRef |  5.6685 ns | 0.1391 ns | 0.2473 ns |  5.6485 ns |      - |     - |     - |         - |
|        WhereAreTheAllocationsComingFrom |  0.0198 ns | 0.0214 ns | 0.0263 ns |  0.0059 ns |      - |     - |     - |         - |

// FINAL - only logical fix, versus random ideas trying to remove allocations, ended up being all that was necessary
|                                  Method |       Mean |     Error |    StdDev |     Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------------- |-----------:|----------:|----------:|-----------:|-------:|------:|------:|----------:|
|                                MapValue |  5.6851 ns | 0.1575 ns | 0.3744 ns |  5.7010 ns |      - |     - |     - |         - |
|                             MapValueOld | 11.8422 ns | 0.2781 ns | 0.4410 ns | 11.8995 ns | 0.0153 |     - |     - |      64 B |
|                    MapValueWithoutLocal |  6.9262 ns | 0.1839 ns | 0.3498 ns |  6.8714 ns |      - |     - |     - |         - |
| MapValueWithoutLocalAndConstantDelegate | 13.6697 ns | 0.3214 ns | 0.7254 ns | 13.6166 ns | 0.0153 |     - |     - |      64 B |
|             MapValueWithoutGenericValue |  5.7967 ns | 0.1558 ns | 0.3793 ns |  5.7977 ns |      - |     - |     - |         - |
|                 MapValueWithoutGenerics |  5.7128 ns | 0.1553 ns | 0.3981 ns |  5.7344 ns |      - |     - |     - |         - |
|                 MapValueWithoutDelegate |  4.7559 ns | 0.1368 ns | 0.2856 ns |  4.7914 ns |      - |     - |     - |         - |
|                         MapValueWithRef |  7.1245 ns | 0.1874 ns | 0.4153 ns |  7.0970 ns |      - |     - |     - |         - |
|        WhereAreTheAllocationsComingFrom |  0.0273 ns | 0.0245 ns | 0.0501 ns |  0.0000 ns |      - |     - |     - |         - |
         */

        [Benchmark]
        public void MapValue() {
            ValueMapper.MapValue(entity.Name, model.Name, model, (model, v) => model.Name = v);
        }

        [Benchmark]
        public void MapValueOld() {
            MapValueOld(entity.Name, model.Name, v => model.Name = v);
        }

        [Benchmark]
        public void MapValueWithoutLocal() {
            MapValueWithoutLocalFunction(entity.Name, model.Name, model, (t, v) => t.Name = v);
        }

        [Benchmark]
        public void MapValueWithoutLocalAndConstantDelegate() {
            MapValueWithoutLocalFunction(entity.Name, model.Name, model, MapNameValue);
        }

        [Benchmark]
        public void MapValueWithoutGenericValue() {
            MapValueWithoutGenericValue(entity.Name, model.Name, model, (t, v) => t.Name = v);
        }

        [Benchmark]
        public void MapValueWithoutGenerics() {
            MapValueWithoutGenerics(entity.Name, model.Name, model, (t, v) => t.Name = v);
        }

        [Benchmark]
        public void MapValueWithoutDelegate() {
            MapValueWithoutDelegate(entity.Name, model.Name, model);
        }

        [Benchmark]
        public void MapValueWithRef() {
            var name = model.Name;
            MapValueWithRef(entity.Name, ref name);
            model.Name = name;
        }

        [Benchmark]
        public void WhereAreTheAllocationsComingFrom() {
        }

        private static void MapNameValue(ModelA Model, string Name) => Model.Name = Name;
        static bool MapValueOld<TValue>(TValue SourceValue, TValue TargetValue, Action<TValue> TargetSetter) {
            //// Validate rules if they exist, if not, treat as valid
            //if (FieldRules?.Invoke(Source, Target) ?? true) {
            if (SourceValue == null) {
                //if (!IgnoreNulls) {
                if (TargetValue != null) {
                    TargetSetter(default);
                    return true;
                } else {
                    return false;
                }
                //}
            } else if (TargetValue == null) {
                // if source != null and target == null, then set
                TargetSetter(SourceValue);
                return true;
            } else {
                // if different value, then set
                if (!SourceValue.Equals(TargetValue)) {
                    TargetSetter(SourceValue);
                    return true;
                }
            }
            //}

            return false;
        }

        static bool MapValueWithoutLocalFunction<TValue, TTarget>(TValue SourceValue, TValue TargetValue, TTarget Target, Action<TTarget, TValue> TargetSetter) {
            //// Validate rules if they exist, if not, treat as valid
            //if (FieldRules?.Invoke(Source, Target) ?? true) {
            if (SourceValue == null) {
                //if (!IgnoreNulls) {
                if (TargetValue != null) {
                    TargetSetter(Target, default);
                    return true;
                } else {
                    return false;
                }
                //}
            } else if (TargetValue == null) {
                // if source != null and target == null, then set
                TargetSetter(Target, SourceValue);
                return true;
            } else {
                // if different value, then set
                if (!SourceValue.Equals(TargetValue)) {
                    TargetSetter(Target, SourceValue);
                    return true;
                }
            }
            //}

            return false;
        }

        static bool MapValueWithoutGenericValue<TTarget>(string SourceValue, string TargetValue, TTarget Target, Action<TTarget, string> TargetSetter) {
            //// Validate rules if they exist, if not, treat as valid
            //if (FieldRules?.Invoke(Source, Target) ?? true) {
            if (SourceValue == null) {
                //if (!IgnoreNulls) {
                if (TargetValue != null) {
                    TargetSetter(Target, default);
                    return true;
                } else {
                    return false;
                }
                //}
            } else if (TargetValue == null) {
                // if source != null and target == null, then set
                TargetSetter(Target, SourceValue);
                return true;
            } else {
                // if different value, then set
                if (!SourceValue.Equals(TargetValue)) {
                    TargetSetter(Target, SourceValue);
                    return true;
                }
            }
            //}

            return false;
        }

        static bool MapValueWithoutGenerics(string SourceValue, string TargetValue, ModelA Target, Action<ModelA, string> TargetSetter) {
            //// Validate rules if they exist, if not, treat as valid
            //if (FieldRules?.Invoke(Source, Target) ?? true) {
            if (SourceValue == null) {
                //if (!IgnoreNulls) {
                if (TargetValue != null) {
                    TargetSetter(Target, default);
                    return true;
                } else {
                    return false;
                }
                //}
            } else if (TargetValue == null) {
                // if source != null and target == null, then set
                TargetSetter(Target, SourceValue);
                return true;
            } else {
                // if different value, then set
                if (!SourceValue.Equals(TargetValue)) {
                    TargetSetter(Target, SourceValue);
                    return true;
                }
            }
            //}

            return false;
        }

        static bool MapValueWithoutDelegate(string SourceValue, string TargetValue, ModelA Target) {
            //// Validate rules if they exist, if not, treat as valid
            //if (FieldRules?.Invoke(Source, Target) ?? true) {
            if (SourceValue == null) {
                //if (!IgnoreNulls) {
                if (TargetValue != null) {
                    Target.Name = default;
                    return true;
                } else {
                    return false;
                }
                //}
            } else if (TargetValue == null) {
                // if source != null and target == null, then set
                Target.Name = SourceValue;
                return true;
            } else {
                // if different value, then set
                if (!SourceValue.Equals(TargetValue)) {
                    Target.Name = SourceValue;
                    return true;
                }
            }
            //}

            return false;
        }

        static bool MapValueWithRef(string SourceValue, ref string TargetValue) {
            //// Validate rules if they exist, if not, treat as valid
            //if (FieldRules?.Invoke(Source, Target) ?? true) {
            if (SourceValue == null) {
                //if (!IgnoreNulls) {
                if (TargetValue != null) {
                    TargetValue = default;
                    return true;
                } else {
                    return false;
                }
                //}
            } else if (TargetValue == null) {
                // if source != null and target == null, then set
                TargetValue = SourceValue;
                return true;
            } else {
                // if different value, then set
                if (!SourceValue.Equals(TargetValue)) {
                    TargetValue = SourceValue;
                    return true;
                }
            }
            //}

            return false;
        }

        class EntityA {
            public string Name { get; set; }
        }

        class ModelA {
            public string Name { get; set; }
        }
    }
}
