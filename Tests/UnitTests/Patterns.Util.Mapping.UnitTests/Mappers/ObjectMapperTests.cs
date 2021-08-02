using Patterns.Util.Mapping.Mappers;
using Xunit;

namespace Patterns.Util.Mapping.UnitTests {
    public class ObjectMapperTests {
        [Fact]
        public void ShouldSetValue() {
            var e = new EntityA() { Child = new EntityB() { Name = nameof(EntityB) } };
            var m = new ModelA();

            var result = ObjectMapper.MapObject(e.Child, m.Child, () => new ModelB(), m, (m, v) => m.Child = v, Map);

            Assert.True(result);
            Assert.NotNull(m.Child);
            Assert.Equal(e.Child.Name, m.Child.Name);
        }

        [Fact]
        public void ShouldSetNewValue() {
            var e = new EntityA() { Child = new EntityB() { Name = nameof(EntityB) } };
            var m = new ModelA() { Child = new ModelB() { Name = nameof(ModelB) } };

            var result = ObjectMapper.MapObject(e.Child, m.Child, () => new ModelB(), m, (m, v) => m.Child = v, Map);

            Assert.True(result);
            Assert.NotNull(m.Child);
            Assert.Equal(e.Child.Name, m.Child.Name);
        }

        [Fact]
        public void ShouldSetNullValue() {
            var e = new EntityA();
            var m = new ModelA() { Child = new ModelB() { Name = nameof(ModelB) } };

            var result = ObjectMapper.MapObject(e.Child, m.Child, () => new ModelB(), m, (m, v) => m.Child = v, Map);

            Assert.True(result);
            Assert.Null(m.Child);
        }

        [Fact]
        public void ShouldNotSetNewValue() {
            var e = new EntityA() { Child = new EntityB() { Name = nameof(EntityB) } };
            var m = new ModelA() { Child = new ModelB() { Name = nameof(EntityB) } };

            var result = ObjectMapper.MapObject(e.Child, m.Child, () => new ModelB(), m, (m, v) => m.Child = v, Map);

            Assert.False(result);
            Assert.NotNull(m.Child);
            Assert.Equal(e.Child.Name, m.Child.Name);
        }

        class EntityA {
            public EntityB Child { get; set; }
        }
        class EntityB {
            public string Name { get; set; }
        }

        class ModelA {
            public ModelB Child { get; set; }
        }
        class ModelB {
            public string Name { get; set; }
        }

        static bool Map(EntityB Source, ModelB Target) => ValueMapper.MapValue(Source.Name, Target.Name, Target, (t, v) => t.Name = v);
    }
}
