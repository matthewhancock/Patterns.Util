using Patterns.Util.Mapping.Mappers;
using Xunit;

namespace Patterns.Util.Mapping.UnitTests {
    public class ValueMapperTests {
        [Fact]
        public void ShouldSetValue() {
            var e = new EntityA() { Name = nameof(EntityA) };
            var m = new ModelA();

            var result = ValueMapper.MapValue(e.Name, m.Name, m, (m, v) => m.Name = v);

            Assert.True(result);
            Assert.Equal(e.Name, m.Name);
        }

        [Fact]
        public void ShouldSetNewValue() {
            var e = new EntityA() { Name = nameof(EntityA) };
            var m = new ModelA() { Name = nameof(ModelA) };

            var result = ValueMapper.MapValue(e.Name, m.Name, m, (m, v) => m.Name = v);

            Assert.True(result);
            Assert.Equal(e.Name, m.Name);
        }

        [Fact]
        public void ShouldSetNullValue() {
            var e = new EntityA();
            var m = new ModelA() { Name = nameof(ModelA) };

            var result = ValueMapper.MapValue(e.Name, m.Name, m, (m, v) => m.Name = v);

            Assert.True(result);
            Assert.Null(m.Name);
        }

        [Fact]
        public void ShouldNotSetNewValue() {
            var e = new EntityA() { Name = nameof(EntityA) };
            var m = new ModelA() { Name = nameof(EntityA) };

            var result = ValueMapper.MapValue(e.Name, m.Name, m, (m, v) => m.Name = v);

            Assert.False(result);
            Assert.Equal(e.Name, m.Name);
        }

        class EntityA {
            public string Name { get; set; }
        }


        class ModelA {
            public string Name { get; set; }
        }
    }
}
