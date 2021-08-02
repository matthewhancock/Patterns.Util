using System.Collections.Generic;
using System.Linq;
using Patterns.Util.Mapping.Mappers;
using Xunit;

namespace Patterns.Util.Mapping.UnitTests {
    public class EnumerableMapperTests {

        [Fact]
        public void ShouldSetValue() {
            var e = new EntityA() { Enumerable = new[] { 1 } };
            var m = new ModelA();

            var result = EnumerableMapper.MapEnumerable(e.Enumerable, m.Enumerable, m, (m, v) => m.Enumerable = v);

            Assert.True(result);
            Assert.NotNull(m.Enumerable);
            Assert.Equal(e.Enumerable.Count(), m.Enumerable.Count());
            Assert.Equal(e.Enumerable.First(), m.Enumerable.First());
        }

        [Fact]
        public void ShouldSetNewValue() {
            var e = new EntityA() { Enumerable = new[] { 1 } };
            var m = new ModelA() { Enumerable = new[] { 2 } };

            var result = EnumerableMapper.MapEnumerable(e.Enumerable, m.Enumerable, m, (m, v) => m.Enumerable = v);

            Assert.True(result);
            Assert.NotNull(m.Enumerable);
            Assert.Equal(e.Enumerable.Count(), m.Enumerable.Count());
            Assert.Equal(e.Enumerable.First(), m.Enumerable.First());
        }

        [Fact]
        public void ShouldSetNullValue() {
            var e = new EntityA();
            var m = new ModelA() { Enumerable = new[] { 1 } };

            var result = EnumerableMapper.MapEnumerable(e.Enumerable, m.Enumerable, m, (m, v) => m.Enumerable = v);

            Assert.True(result);
            Assert.Null(m.Enumerable);
        }

        [Fact]
        public void ShouldNotSetNewValue() {
            var e = new EntityA() { Enumerable = new[] { 1 } };
            var m = new ModelA() { Enumerable = new[] { 1 } };

            var result = EnumerableMapper.MapEnumerable(e.Enumerable, m.Enumerable, m, (m, v) => m.Enumerable = v);

            Assert.False(result);
            Assert.NotNull(m.Enumerable);
            Assert.Equal(e.Enumerable.Count(), m.Enumerable.Count());
            Assert.Equal(e.Enumerable.First(), m.Enumerable.First());
        }

        [Fact]
        public void ShouldAddNewItem() {
            var e = new EntityA() { Enumerable = new[] { 1, 2 } };
            var m = new ModelA() { Enumerable = new[] { 1 } };

            var result = EnumerableMapper.MapEnumerable(e.Enumerable, m.Enumerable, m, (m, v) => m.Enumerable = v);

            Assert.True(result);
            Assert.NotNull(m.Enumerable);
            Assert.Equal(e.Enumerable.Count(), m.Enumerable.Count());
            Assert.Equal(e.Enumerable.First(), m.Enumerable.First());
            Assert.Equal(e.Enumerable.Skip(1).First(), m.Enumerable.Skip(1).First());
        }

        [Fact]
        public void ShouldRemoveMissingItem() {
            var e = new EntityA() { Enumerable = new[] { 1 } };
            var m = new ModelA() { Enumerable = new[] { 1, 2 } };

            var result = EnumerableMapper.MapEnumerable(e.Enumerable, m.Enumerable, m, (m, v) => m.Enumerable = v);

            Assert.True(result);
            Assert.NotNull(m.Enumerable);
            Assert.Equal(e.Enumerable.Count(), m.Enumerable.Count());
            Assert.Equal(e.Enumerable.First(), m.Enumerable.First());
        }

        [Fact]
        public void ShouldSetComplexValue() {
            var e = new EntityA() { EnumerableComplex = new[] { new EntityB() { ID = 1 } } };
            var m = new ModelA();

            var result = EnumerableMapper.MapEnumerable(e.EnumerableComplex, m.EnumerableComplex, s => s.ID, t => t.ID, () => new ModelB(),
                    m, (m, v) => m.EnumerableComplex = v, Map);

            Assert.True(result);
            Assert.NotNull(m.EnumerableComplex);
            Assert.Equal(e.EnumerableComplex.Count(), m.EnumerableComplex.Count());
            Assert.Equal(e.EnumerableComplex.First().ID, m.EnumerableComplex.First().ID);
        }

        [Fact]
        public void ShouldSetNewComplexValue() {
            var e = new EntityA() { EnumerableComplex = new[] { new EntityB() { ID = 1 } } };
            var m = new ModelA() { EnumerableComplex = new[] { new ModelB() { ID = 2 } } };

            var result = EnumerableMapper.MapEnumerable(e.EnumerableComplex, m.EnumerableComplex, s => s.ID, t => t.ID, () => new ModelB(),
                    m, (m, v) => m.EnumerableComplex = v, Map);

            Assert.True(result);
            Assert.NotNull(m.EnumerableComplex);
            Assert.Equal(e.EnumerableComplex.Count(), m.EnumerableComplex.Count());
            Assert.Equal(e.EnumerableComplex.First().ID, m.EnumerableComplex.First().ID);
        }

        [Fact]
        public void ShouldSetNullComplexValue() {
            var e = new EntityA();
            var m = new ModelA() { EnumerableComplex = new[] { new ModelB() { ID = 1 } } };

            var result = EnumerableMapper.MapEnumerable(e.EnumerableComplex, m.EnumerableComplex, s => s.ID, t => t.ID, () => new ModelB(),
                    m, (m, v) => m.EnumerableComplex = v, Map);

            Assert.True(result);
            Assert.Null(m.EnumerableComplex);
        }

        [Fact]
        public void ShouldNotSetNewComplexValue() {
            var e = new EntityA() { EnumerableComplex = new[] { new EntityB() { ID = 1 } } };
            var m = new ModelA() { EnumerableComplex = new[] { new ModelB() { ID = 1 } } };

            var result = EnumerableMapper.MapEnumerable(e.EnumerableComplex, m.EnumerableComplex, s => s.ID, t => t.ID, () => new ModelB(),
                    m, (m, v) => m.EnumerableComplex = v, Map);

            Assert.False(result);
            Assert.NotNull(m.EnumerableComplex);
            Assert.Equal(e.EnumerableComplex.Count(), m.EnumerableComplex.Count());
            Assert.Equal(e.EnumerableComplex.First().ID, m.EnumerableComplex.First().ID);
        }

        [Fact]
        public void ShouldAddNewComplexItem() {
            var e = new EntityA() { EnumerableComplex = new EntityB[] { new() { ID = 1 }, new() { ID = 2 } } };
            var m = new ModelA() { EnumerableComplex = new ModelB[] { new() { ID = 1 } } };

            var result = EnumerableMapper.MapEnumerable(e.EnumerableComplex, m.EnumerableComplex, s => s.ID, t => t.ID, () => new ModelB(),
                    m, (m, v) => m.EnumerableComplex = v, Map);

            Assert.True(result);
            Assert.NotNull(m.EnumerableComplex);
            Assert.Equal(e.EnumerableComplex.Count(), m.EnumerableComplex.Count());
            Assert.Equal(e.EnumerableComplex.First().ID, m.EnumerableComplex.First().ID);
            Assert.Equal(e.EnumerableComplex.Skip(1).First().ID, m.EnumerableComplex.Skip(1).First().ID);
        }

        [Fact]
        public void ShouldRemoveMissingComplexItem() {
            var e = new EntityA() { EnumerableComplex = new EntityB[] { new() { ID = 1 } } };
            var m = new ModelA() { EnumerableComplex = new ModelB[] { new() { ID = 1 }, new() { ID = 2 } } };

            var result = EnumerableMapper.MapEnumerable(e.EnumerableComplex, m.EnumerableComplex, s => s.ID, t => t.ID, () => new ModelB(),
                    m, (m, v) => m.EnumerableComplex = v, Map);

            Assert.True(result);
            Assert.NotNull(m.EnumerableComplex);
            Assert.Equal(e.EnumerableComplex.Count(), m.EnumerableComplex.Count());
            Assert.Equal(e.EnumerableComplex.First().ID, m.EnumerableComplex.First().ID);
        }

        class EntityA {
            public IEnumerable<int> Enumerable { get; set; }
            public IEnumerable<EntityB> EnumerableComplex { get; set; }
        }
        class EntityB {
            public int ID { get; set; }
        }

        class ModelA {
            public IEnumerable<int> Enumerable { get; set; }
            public IEnumerable<ModelB> EnumerableComplex { get; set; }
        }
        class ModelB {
            public int ID { get; set; }
        }

        static bool Map(EntityB Source, ModelB Target)
            => ValueMapper.MapValue(Source.ID, Target.ID, Target, (t, v) => t.ID = v);
    }
}
