using System.Collections.Generic;
using System.Linq;
using Patterns.Util.Mapping.Mappers;
using Xunit;

namespace Patterns.Util.Mapping.UnitTests {
    public class ReferenceMapperTests {
        private static readonly Dictionary<int, ModelC> mapFromReferenceLookup = new() {
            [1] = new() { ID = 1 },
            [2] = new() { ID = 2 }
        };

        [Fact]
        public void ShouldSetValue() {
            var e = new EntityA() { EnumerableItems = new[] { new EntityB() { ID = 1 } } };
            var m = new ModelA();

            var result = ReferenceMapper.MapReference(e.EnumerableItems, s => s.ID, m.EnumerableItemsReference, m,
                (m, v) => m.EnumerableItemsReference = v);

            Assert.True(result);
            Assert.NotNull(m.EnumerableItemsReference);
            Assert.Equal(e.EnumerableItems.Count(), m.EnumerableItemsReference.Count());
            Assert.Equal(e.EnumerableItems.First().ID, m.EnumerableItemsReference.First());
        }

        [Fact]
        public void ShouldSetNewValue() {
            var e = new EntityA() { EnumerableItems = new[] { new EntityB() { ID = 1 } } };
            var m = new ModelA() { EnumerableItemsReference = new[] { 2 } };

            var result = ReferenceMapper.MapReference(e.EnumerableItems, s => s.ID, m.EnumerableItemsReference, m,
                (m, v) => m.EnumerableItemsReference = v);

            Assert.True(result);
            Assert.NotNull(m.EnumerableItemsReference);
            Assert.Equal(e.EnumerableItems.Count(), m.EnumerableItemsReference.Count());
            Assert.Equal(e.EnumerableItems.First().ID, m.EnumerableItemsReference.First());
        }

        [Fact]
        public void ShouldSetNullValue() {
            var e = new EntityA();
            var m = new ModelA() { EnumerableItemsReference = new[] { 2 } };

            var result = ReferenceMapper.MapReference(e.EnumerableItems, s => s.ID, m.EnumerableItemsReference, m,
                (m, v) => m.EnumerableItemsReference = v);

            Assert.True(result);
            Assert.Null(m.EnumerableItemsReference);
        }

        [Fact]
        public void ShouldNotSetNewValue() {
            var e = new EntityA() { EnumerableItems = new[] { new EntityB() { ID = 1 } } };
            var m = new ModelA() { EnumerableItemsReference = new[] { 1 } };

            var result = ReferenceMapper.MapReference(e.EnumerableItems, s => s.ID, m.EnumerableItemsReference, m,
                (m, v) => m.EnumerableItemsReference = v);

            Assert.False(result);
            Assert.NotNull(m.EnumerableItemsReference);
            Assert.Equal(e.EnumerableItems.Count(), m.EnumerableItemsReference.Count());
            Assert.Equal(e.EnumerableItems.First().ID, m.EnumerableItemsReference.First());
        }


        [Fact]
        public void ShouldAddNewItem() {
            var e = new EntityA() { EnumerableItems = new EntityB[] { new() { ID = 1 }, new() { ID = 2 } } };
            var m = new ModelA() { EnumerableItemsReference = new[] { 1 } };

            var result = ReferenceMapper.MapReference(e.EnumerableItems, s => s.ID, m.EnumerableItemsReference, m,
                (m, v) => m.EnumerableItemsReference = v);

            Assert.True(result);
            Assert.NotNull(m.EnumerableItemsReference);
            Assert.Equal(e.EnumerableItems.Count(), m.EnumerableItemsReference.Count());
            Assert.Equal(e.EnumerableItems.First().ID, m.EnumerableItemsReference.First());
            Assert.Equal(e.EnumerableItems.Skip(1).First().ID, m.EnumerableItemsReference.Skip(1).First());
        }

        [Fact]
        public void ShouldRemoveMissingItem() {
            var e = new EntityA() { EnumerableItems = new EntityB[] { new() { ID = 1 }} };
            var m = new ModelA() { EnumerableItemsReference = new[] { 1, 2 } };

            var result = ReferenceMapper.MapReference(e.EnumerableItems, s => s.ID, m.EnumerableItemsReference, m,
                (m, v) => m.EnumerableItemsReference = v);

            Assert.True(result);
            Assert.NotNull(m.EnumerableItemsReference);
            Assert.Equal(e.EnumerableItems.Count(), m.EnumerableItemsReference.Count());
            Assert.Equal(e.EnumerableItems.First().ID, m.EnumerableItemsReference.First());
        }


        [Fact]
        public void ShouldSetComplexValue() {
            var e = new EntityC() { EnumerableItemsReference = new[] { 1 } };
            var m = new ModelB();

            var result = ReferenceMapper.MapFromReference(e.EnumerableItemsReference, m.EnumerableItems, s => s.ID,
                m, (m, v) => m.EnumerableItems = v, mapFromReferenceLookup);

            Assert.True(result);
            Assert.NotNull(m.EnumerableItems);
            Assert.Equal(e.EnumerableItemsReference.Count(), m.EnumerableItems.Count());
            Assert.Equal(e.EnumerableItemsReference.First(), m.EnumerableItems.First().ID);
        }

        [Fact]
        public void ShouldSetNewComplexValue() {
            var e = new EntityC() { EnumerableItemsReference = new[] { 1 } };
            var m = new ModelB() { EnumerableItems = new ModelC[] { new() { ID = 2 } } };

            var result = ReferenceMapper.MapFromReference(e.EnumerableItemsReference, m.EnumerableItems, s => s.ID,
                m, (m, v) => m.EnumerableItems = v, mapFromReferenceLookup);

            Assert.True(result);
            Assert.NotNull(m.EnumerableItems);
            Assert.Equal(e.EnumerableItemsReference.Count(), m.EnumerableItems.Count());
            Assert.Equal(e.EnumerableItemsReference.First(), m.EnumerableItems.First().ID);
        }

        [Fact]
        public void ShouldSetNullComplexValue() {
            var e = new EntityC();
            var m = new ModelB() { EnumerableItems = new ModelC[] { new() { ID = 1 } } };

            var result = ReferenceMapper.MapFromReference(e.EnumerableItemsReference, m.EnumerableItems, s => s.ID,
                m, (m, v) => m.EnumerableItems = v, mapFromReferenceLookup);

            Assert.True(result);
            Assert.Null(m.EnumerableItems);
        }

        [Fact]
        public void ShouldNotSetNewComplexValue() {
            var e = new EntityC() { EnumerableItemsReference = new[] { 1 } };
            var m = new ModelB() { EnumerableItems = new ModelC[] { new() { ID = 1 } } };

            var result = ReferenceMapper.MapFromReference(e.EnumerableItemsReference, m.EnumerableItems, s => s.ID,
                m, (m, v) => m.EnumerableItems = v, mapFromReferenceLookup);

            Assert.False(result);
            Assert.NotNull(m.EnumerableItems);
            Assert.Equal(e.EnumerableItemsReference.Count(), m.EnumerableItems.Count());
            Assert.Equal(e.EnumerableItemsReference.First(), m.EnumerableItems.First().ID);
        }


        [Fact]
        public void ShouldAddNewComplexItem() {
            var e = new EntityC() { EnumerableItemsReference = new[] { 1, 2 } };
            var m = new ModelB() { EnumerableItems = new ModelC[] { new() { ID = 1 } } };

            var result = ReferenceMapper.MapFromReference(e.EnumerableItemsReference, m.EnumerableItems, s => s.ID,
                m, (m, v) => m.EnumerableItems = v, mapFromReferenceLookup);

            Assert.True(result);
            Assert.NotNull(m.EnumerableItems);
            Assert.Equal(e.EnumerableItemsReference.Count(), m.EnumerableItems.Count());
            Assert.Equal(e.EnumerableItemsReference.First(), m.EnumerableItems.First().ID);
            Assert.Equal(e.EnumerableItemsReference.Skip(1).First(), m.EnumerableItems.Skip(1).First().ID);
        }

        [Fact]
        public void ShouldRemoveMissingComplexItem() {
            var e = new EntityC() { EnumerableItemsReference = new[] { 2 } };
            var m = new ModelB() { EnumerableItems = new ModelC[] { new() { ID = 1 }, new() { ID = 2 } } };

            var result = ReferenceMapper.MapFromReference(e.EnumerableItemsReference, m.EnumerableItems, s => s.ID,
                m, (m, v) => m.EnumerableItems = v, mapFromReferenceLookup);

            Assert.True(result);
            Assert.NotNull(m.EnumerableItems);
            Assert.Equal(e.EnumerableItemsReference.Count(), m.EnumerableItems.Count());
            Assert.Equal(e.EnumerableItemsReference.First(), m.EnumerableItems.First().ID);
        }

        // Complex >> Reference
        class EntityA {
            public IEnumerable<EntityB> EnumerableItems { get; set; }
        }
        class EntityB {
            public int ID { get; set; }
        }

        class ModelA {
            public IEnumerable<int> EnumerableItemsReference { get; set; }
        }

        // Reference >> Complex
        class EntityC {
            public IEnumerable<int> EnumerableItemsReference { get; set; }
        }
        class ModelB {
            public IEnumerable<ModelC> EnumerableItems { get; set; }
        }
        class ModelC {
            public int ID { get; set; }
        }

    }
}
